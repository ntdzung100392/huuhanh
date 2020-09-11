using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Forms.Core.Data.Storage;
using Umbraco.Forms.Core.Models;
using Umbraco.Forms.Core.Providers.FieldTypes;

namespace HHCoApps.CMSWeb.Forms.FieldTypes
{
    public class ImageFileUpload : FileUpload
    {
        private const int MaxSizeUpload = 2097152;

        public ImageFileUpload()
        {
            Id = Guid.Parse("58567E45-4055-458E-86F3-5033985D24EE");
            Name = "Image upload";
            Description = "Renders an image upload field with a max size limit.";
            Icon = "icon-cloud-upload";
            FieldTypeViewName = "FieldType.ImageFileUpload.cshtml";
        }

        public override IEnumerable<string> ValidateField(Form form, Field field, IEnumerable<object> postedValues, HttpContextBase context, IFormStorage formStorage)
        {
            var validateResults = base.ValidateField(form, field, postedValues, context, formStorage).ToList();
            var files = context.Request.Files.GetMultiple(field.Id.ToString());

            foreach (HttpPostedFileBase file in files.Where(x => x.ContentLength > 0))
            {
                if (file.ContentLength > MaxSizeUpload)
                {
                    validateResults.Add(file.FileName + " is too large. Max 2MB is allowed.");
                }

                if (!file.ContentType.Contains("image"))
                {
                    validateResults.Add("Please upload valid image");
                }

                bool extensionValid = new[] { "png", "jpg", "jpeg", "gif" }.Any(fileExtension => file.ContentType.Contains(fileExtension));
                if (!extensionValid)
                {
                    validateResults.Add("Image extension not valid. Please choose *.png / *.jpeg / *.gif / *.jpg");
                }
            }

            return validateResults;
        }
    }
}