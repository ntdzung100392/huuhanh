using System;
using Umbraco.Forms.Core;
using Umbraco.Forms.Core.Attributes;
using Umbraco.Forms.Core.Enums;

namespace HHCoApps.CMSWeb.Forms.FieldTypes
{
    public class TextAreaEditor : FieldType
    {
        [Setting("Caption", Description = "Enter a caption")]
        public string Caption { get; set; }

        [Setting("Body Text", Description = "Enter your text", View = "richtext")]
        public string BodyText { get; set; }

        [Setting("Text Align", Description = "Choose your text alignment", PreValues = "Left,Center,Right", View = "dropdownlist")]
        public string TextAlign { get; set; }

        public TextAreaEditor()
        {
            this.Id = new Guid("501d5aa0-5c72-4573-874d-f2b0bef1919f");
            this.Name = "Text Area Editor";
            this.Description = "This is used to enter some text";
            this.Icon = "icon-edit";
            this.DataType = FieldDataType.String;
            this.Category = "Simple";
            this.SortOrder = 100;
            this.FieldTypeViewName = "FieldType.TextAreaEditor.cshtml";
        }

        public override bool HideLabel
        {
            get
            {
                return true;
            }
        }

        public override bool StoresData
        {
            get
            {
                return false;
            }
        }
    }
}