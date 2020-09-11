using HHCoApps.CMSWeb.Caching;
using DuluxGroup.Integration.Mailchimp;
using DuluxGroup.Integration.Mailchimp.Models;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Forms.Core;
using Umbraco.Forms.Core.Data.Storage;
using Umbraco.Forms.Core.Enums;
using Umbraco.Forms.Core.Persistence.Dtos;
using Umbraco.Web;
using Umbraco.Web.Composing;

namespace HHCoApps.CMSWeb.Forms.Workflows
{
    public class MailchimpSubmitSubscribeFormWorkflow : WorkflowType
    {
        private const string Mailchimp = "Mailchimp";
        private const string ExtensionDataFieldName = "extensionData";

        private readonly IRecordStorage _recordStorage;
        private readonly ILogger _logger;

        public MailchimpSubmitSubscribeFormWorkflow(IRecordStorage recordStorage)
        {
            _recordStorage = recordStorage;
            _logger = Current.Logger;
            Id = Guid.Parse("4ee1bbf1-91b8-4d89-99c7-41ef5f93d9e8");

            Name = "Mailchimp - Submit Subscribe Form";
        }

        public override WorkflowExecutionStatus Execute(Record record, RecordEventArgs recordEventArgs)
        {
            try
            {
                var extensionData = record.GetValue<string>(ExtensionDataFieldName);
                if (string.IsNullOrEmpty(extensionData))
                {
                    SendSubscribeForm(record, recordEventArgs);
                }

                return WorkflowExecutionStatus.Completed;
            }
            catch (Exception exception)
            {
                MarkAsFailed(record, recordEventArgs);

                _logger.Error<MailchimpSubmitSubscribeFormWorkflow>(exception, "Cannot submit contact form to {Integration}", Mailchimp);
                return WorkflowExecutionStatus.Failed;
            }
        }

        private void SendSubscribeForm(Record record, RecordEventArgs recordEventArgs)
        {
            var umbracoHelper = Current.Factory.GetInstance<UmbracoHelper>();
            var integration = (Umbraco.Web.PublishedModels.Integration)umbracoHelper.ContentQuery.ContentSingleFromCache(CachedContent.IntegrationContent);
            var subscribeForm = GetSubscribeForm(record, integration, recordEventArgs);
            var connectionInformation = GetConnectionInformation(integration);

            Task.Factory.StartNew(() =>
            {
                try
                {
                    var mailchimpService = Current.Factory.GetInstance<IMailchimpService>();
                    mailchimpService.Client.ConnectionInformation = connectionInformation;
                    mailchimpService.Subscribe(subscribeForm);

                    MarkAsProcessed(record, recordEventArgs);

                    _logger.Info<MailchimpSubmitSubscribeFormWorkflow>(
                        "Subscribe form with recordId {RecordId} is successfully submitted to {Integration}",
                        record.UniqueId, Mailchimp);
                }
                catch (Exception exception)
                {
                    var responseText = string.Empty;
                    if (exception.InnerException is FlurlHttpException flurlHttpException)
                    {
                        responseText = flurlHttpException.Call?.Response?.ToString();
                    }

                    MarkAsFailed(record, recordEventArgs);

                    _logger.Error<MailchimpSubmitSubscribeFormWorkflow>(
                        exception,
                        "Subscribe form with recordId {RecordId} is failed to submit to {Integration}. ResponseText: {ResponseText}.",
                        record.UniqueId, Mailchimp, responseText);
                }
            });
        }

        private void MarkAsFailed(Record record, RecordEventArgs recordEventArgs)
        {
            record.State = FormState.Submitted;
            InsertOrUpdateRecord(record, recordEventArgs);
        }

        private void MarkAsProcessed(Record record, RecordEventArgs recordEventArgs)
        {
            var extensionDataRecordField = record.GetRecordFieldByAlias(ExtensionDataFieldName);
            extensionDataRecordField.Values = new List<object>
            {
                "Mailchimp subscribed"
            };

            record.State = FormState.Approved;
            InsertOrUpdateRecord(record, recordEventArgs);
        }

        private ConnectionInformation GetConnectionInformation(Umbraco.Web.PublishedModels.Integration integration)
        {
            return new ConnectionInformation
            {
                BaseAddressUrl = string.Format(ConfigurationManager.AppSettings["MAILCHIMP_API_BASEURL"], integration.MailchimpServer),
                BasicAuth = new BasicAuth("anystring", integration.MailchimpApiKey)
            };
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

        private SubscribeForm GetSubscribeForm(Record record, Umbraco.Web.PublishedModels.Integration integration, RecordEventArgs recordEventArgs)
        {
            var mappingData = new MappingData
            {
                ListId = integration.MailchimpListId
            };

            return new SubscribeForm
            {
                EmailAddress = record.GetValue<string>("emailAddress"),
                MappingData = mappingData
            };
        }

        public override List<Exception> ValidateSettings()
        {
            return new List<Exception>();
        }
    }
}