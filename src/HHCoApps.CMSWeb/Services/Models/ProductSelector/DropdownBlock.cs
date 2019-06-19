using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HHCoApps.CMSWeb.Services.Models.ProductSelector
{
    public class DropdownBlock : BaseBlock
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public IEnumerable<Dropdown> Dropdowns { get; set; }
    }

    public class Dropdown
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public string MappedLabel { get; set; }
        public string PlaceholderText { get; set; }
        public Dictionary<string, IEnumerable<string>> DisplayCondition { get; set; }
        public IList<DropdownOption> Options { get; set; }
    }

    public class DropdownOption
    {
        public string Id { get; set; }
        public Dictionary<string, IEnumerable<string>> DisplayCondition { get; set; }
        public string DisplayName { get; set; }
        public string Value { get; set; }
    }
}