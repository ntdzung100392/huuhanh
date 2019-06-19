namespace HHCoApps.CMSWeb.Services.Models
{
    public class FilterCriterionModel
    {
        public string FilterCategoryKey { get; set; }
        public string FilterValue { get; set; }
        public bool IsPrimaryFilter { get; set; }
        public bool IsPrimarySubFilter { get; set; }
        public bool IsGroupAnd { get; set; }
    }
}