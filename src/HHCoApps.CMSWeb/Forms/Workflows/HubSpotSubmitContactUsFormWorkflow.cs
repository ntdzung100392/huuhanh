using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using Flurl.Http;
using DuluxGroup.Integrations.HubSpot.Models;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Forms.Core;
using Umbraco.Forms.Core.Data.Storage;
using Umbraco.Forms.Core.Enums;
using Umbraco.Forms.Core.Persistence.Dtos;
using Umbraco.Web;
using Umbraco.Web.Composing;
using DuluxGroup.Integrations.HubSpot.Services;
using DuluxGroup.Integrations.HubSpot.Apis.Models;
using System.Runtime.Caching;
using HHCoApps.CMSWeb.Caching;

namespace HHCoApps.CMSWeb.Forms.Workflows
{
    public class HubSpotSubmitContactUsFormWorkflow : WorkflowType
    {
        private const string HubSpot = "HubSpot";
        private const string ExtensionDataFieldName = "extensionData";

        private readonly IRecordStorage _recordStorage;
        private readonly ILogger _logger;

        public HubSpotSubmitContactUsFormWorkflow(IRecordStorage recordStorage)
        {
            _recordStorage = recordStorage;
            _logger = Current.Logger;
            Id = Guid.Parse("C4EA576A-3DDB-4B92-885B-711181593E85");

            Name = "HubSpot - Submit Contact Us Form";
        }

        public override WorkflowExecutionStatus Execute(Record record, RecordEventArgs recordEventArgs)
        {
            try
            {
                var extensionData = record.GetValue<string>(ExtensionDataFieldName);
                if (string.IsNullOrEmpty(extensionData))
                {
                    SendContactUsForm(record, recordEventArgs);
                }

                return WorkflowExecutionStatus.Completed;
            }
            catch (Exception exception)
            {
                MarkAsFailed(record, recordEventArgs);

                _logger.Error<HubSpotSubmitContactUsFormWorkflow>(exception, "Cannot submit contact form to {Integration}", HubSpot);
                return WorkflowExecutionStatus.Failed;
            }
        }

        public override List<Exception> ValidateSettings()
        {
            return new List<Exception>();
        }

        private void SendContactUsForm(Record record, RecordEventArgs recordEventArgs)
        {
            var umbracoHelper = Current.Factory.GetInstance<UmbracoHelper>();
            var integration = (Umbraco.Web.PublishedModels.Integration)umbracoHelper.ContentQuery.ContentSingleFromCache(CachedContent.IntegrationContent);
            var contactUsForm = GetContactUsForm(record);
            var connectionInformation = GetConnectionInformation();

            Task.Factory.StartNew(() =>
            {
                try
                {
                    var refreshToken = integration.RefreshToken ?? string.Empty;

                    var tokenInfoResponse = CheckMemoryCacheWithTokenInfo(connectionInformation, refreshToken.ToString());
                    var hubspotService = Current.Factory.GetInstance<IHubspotService>();

                    var contactUsResponse = hubspotService.SendContactInfo(contactUsForm, connectionInformation, tokenInfoResponse);
                    var ticketResponse = hubspotService.SendTicketInfo(contactUsForm, connectionInformation, tokenInfoResponse);
                    var associationResponse = hubspotService.CreateContactToTicketAssociation(connectionInformation, tokenInfoResponse, contactUsResponse, ticketResponse);

                    _logger.Info<HubSpotSubmitContactUsFormWorkflow>(
                        "Contact form with recordId {RecordId} submitted to {Integration}",
                        record.UniqueId, HubSpot);

                    MarkAsProcessed(record, recordEventArgs, contactUsResponse, ticketResponse, associationResponse);

                    _logger.Info<HubSpotSubmitContactUsFormWorkflow>(
                        "Contact form with recordId {RecordId} is successfully submitted to {Integration}",
                        record.UniqueId, HubSpot);
                }
                catch (Exception exception)
                {
                    var responseText = string.Empty;
                    if (exception.InnerException is FlurlHttpException flurlHttpException)
                    {
                        responseText = flurlHttpException.Call?.Response?.ToString();
                    }

                    MarkAsFailed(record, recordEventArgs);

                    _logger.Error<HubSpotSubmitContactUsFormWorkflow>(
                        exception,
                        "Contact form with recordId {RecordId} is failed to submit to {Integration}. ResponseText: {ResponseText}.",
                        record.UniqueId, HubSpot, responseText);
                }
            });
        }

        private ConnectionInformation GetConnectionInformation()
        {
            return new ConnectionInformation
            {
                BaseUrl = ConfigurationManager.AppSettings["BASE_URL"],
                ClientId = ConfigurationManager.AppSettings["HubSpotClientId"],
                ClientSecret = ConfigurationManager.AppSettings["CLIENT_SECRET"],
                RedirectUri = ConfigurationManager.AppSettings["REDIRECT_URI"]
            };
        }

        private void MarkAsFailed(Record record, RecordEventArgs recordEventArgs)
        {
            record.State = FormState.Submitted;
            InsertOrUpdateRecord(record, recordEventArgs);
        }

        private void MarkAsProcessed(Record record, RecordEventArgs e, ContactUsResponse contactUsResponse, TicketInfoResponse ticketInfoResponse, AssociationsInfoResponse associationsInfoResponse)
        {
            var extensionDataRecordField = record.GetRecordFieldByAlias(ExtensionDataFieldName);
            var contactStatus = contactUsResponse.IsNew ? "Created" : "Updated";
            extensionDataRecordField.Values = new List<object>
            {
                $"Hubspot Contact Status: {contactStatus}",
                $"Hubspot Contact VisitorId: {contactUsResponse.VisitorId}",
                $"Hubspot Ticket Id: {ticketInfoResponse.Id}",
                $"Hubspot Ticket Created Date: {ticketInfoResponse.CreatedAt}",
                $"Hubspot Associations Status: {associationsInfoResponse.Status}"
            };

            record.State = FormState.Approved;
            InsertOrUpdateRecord(record, e);
        }

        private void InsertOrUpdateRecord(Record record, RecordEventArgs e)
        {
            record.IP ??= string.Empty;
            record.RecordData ??= string.Empty;

            if (record.Created == DateTime.MinValue)
            {
                record.Created = DateTime.Now;
                _recordStorage.InsertRecord(record, e.Form);
            }
            else
            {
                record.Updated = DateTime.Now;
                _recordStorage.UpdateRecord(record, e.Form);
            }
        }

        private ContactUs GetContactUsForm(Record record)
        {
            return new ContactUs
            {
                FirstName = record.GetValue<string>("firstName"),
                LastName = record.GetValue<string>("lastName"),
                PhoneNumber = record.GetValue<string>("phoneNumber"),
                Email = record.GetValue<string>("email"),
                Enquiry = record.GetValue<string>("enquiry")
            };
        }

        private TokenInformation GetTokenInfo(ConnectionInformation connectionInformation, string refreshToken)
        {
            var authenticationService = Current.Factory.GetInstance<IAuthenticationService>();
            authenticationService.AuthenticationApi.SetSeverConnectionInformation(connectionInformation);
            return authenticationService.GetTokenInfomation(refreshToken);
        }

        private TokenInformation CheckMemoryCacheWithTokenInfo(ConnectionInformation connectionInformation, string refreshToken)
        {
            var cacheToken = MemoryCache.Default;
            if (!cacheToken.Contains("accessToken"))
            {
                var tokenInfoResponse = GetTokenInfo(connectionInformation, refreshToken);
                var expiresInMinutes = (tokenInfoResponse.ExpiresIn / 60) - 5;
                cacheToken.Add("accessToken", tokenInfoResponse.AccessToken, DateTimeOffset.Now.AddMinutes(expiresInMinutes));
                cacheToken.Add("expiresIn", tokenInfoResponse.ExpiresIn, DateTimeOffset.Now.AddMinutes(expiresInMinutes));

                return tokenInfoResponse;
            }

            return new TokenInformation
            {
                AccessToken = cacheToken.Get("accessToken").ToString(),
                ExpiresIn = int.Parse(cacheToken.Get("expiresIn").ToString())
            };
        }
    }
}