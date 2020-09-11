using System;
using System.Collections.Generic;
using System.Web;
using HHCoApps.CMSWeb.Caching;
using Umbraco.Forms.Core;
using Umbraco.Forms.Core.Data.Storage;
using Umbraco.Forms.Core.Enums;
using Umbraco.Forms.Core.Models;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Web;
using HHCoApps.CMSWeb.Services;

namespace Recaptcha.Forms
{
    public class Recaptcha3 : FieldType
    {
        public Recaptcha3()
        {
            Id = new Guid("b68a27b8-9f52-4f23-8714-532c1821dc37");
            Name = "Recaptcha3";
            Icon = "icon-eye";
            DataType = FieldDataType.String;
            HideLabel = true;
        }

        public override IEnumerable<string> ValidateField(Form form, Field field, IEnumerable<object> postedValues, HttpContextBase context, IFormStorage formStorage)
        {
            var returnStrings = new List<string>();
            var umbracoHelper = Current.Factory.GetInstance<UmbracoHelper>();
            var recaptchaService = Current.Factory.GetInstance<IRecaptchaService>();
            var integration = (Umbraco.Web.PublishedModels.Integration)umbracoHelper.ContentQuery.ContentSingleFromCache(CachedContent.IntegrationContent);
            var token = HttpContext.Current.Request["g-recaptcha-response"];

            var isValid = recaptchaService.VerifyRecaptcha(token, integration.HumanScore);

            if (!isValid)
            {
                returnStrings.Add("Recaptcha Failed. Try again!");
            }

            return returnStrings;
        }
    }
}