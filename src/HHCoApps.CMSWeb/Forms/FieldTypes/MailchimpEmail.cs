using System;
using System.Collections.Generic;
using System.Web;
using Umbraco.Forms.Core;
using Umbraco.Forms.Core.Data.Storage;
using Umbraco.Forms.Core.Enums;
using Umbraco.Forms.Core.Models;
using Umbraco.Core;
using Umbraco.Core.Composing;
using System.Linq;
using System.Configuration;
using HHCoApps.CMSWeb.Caching;
using Umbraco.Web;
using DuluxGroup.Integration.Mailchimp.Models;
using DuluxGroup.Integration.Mailchimp;
using HHCoApps.CMSWeb.Helpers;

namespace HHCoApps.CMSWeb.Forms.FieldTypes
{
    public class MailchimpEmail : FieldType
    {
        public MailchimpEmail()
        {
            Id = new Guid("dad38094-81df-419b-9d99-5dbfec4fb4d6");
            Name = "MailchimpEmail";
            DataType = FieldDataType.String;
            Icon = "icon-message";
        }

        public override IEnumerable<string> ValidateField(Form form, Field field, IEnumerable<object> postedValues, HttpContextBase context, IFormStorage formStorage)
        {
            var returnStrings = new List<string>();

            if (!postedValues.Any())
                return returnStrings;

            var mailchimpService = Current.Factory.GetInstance<IMailchimpService>();
            var umbracoHelper = Current.Factory.GetInstance<UmbracoHelper>();
            var integration = (Umbraco.Web.PublishedModels.Integration)umbracoHelper.ContentQuery.ContentSingleFromCache(CachedContent.IntegrationContent);

            var connectionInformation = GetConnectionInformation(integration);
            mailchimpService.Client.ConnectionInformation = connectionInformation;

            var email = postedValues.First().ToString();
            var mailchimpListId = integration.MailchimpListId;

            var result = AsyncExtensions.RunSync<bool>(() => mailchimpService.IsEmailAddressSubscribed(email, mailchimpListId));

            if (result)
            {
                returnStrings.Add("You have already subscribed");
            }

            return returnStrings;
        }

        private ConnectionInformation GetConnectionInformation(Umbraco.Web.PublishedModels.Integration integration)
        {
            return new ConnectionInformation
            {
                BaseAddressUrl = string.Format(ConfigurationManager.AppSettings["MAILCHIMP_API_BASEURL"], integration.MailchimpServer),
                BasicAuth = new BasicAuth("anystring", integration.MailchimpApiKey)
            };
        }
    }
}