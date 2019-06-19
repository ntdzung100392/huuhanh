using System;

namespace HHCoApps.CMSWeb.Services.Models.ProductSelector
{
    public class CheckboxBlock : BaseBlock
    {
        public string Id { get; set; }
        public string ConditionKey { get; set; }
        public CheckBoxData Data { get; set; }
    }

    public class CheckBoxData
    {
        public string DisplayName { get; set; }
        public string Value { get; set; }
    }
}