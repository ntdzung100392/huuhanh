namespace HHCoApps.CMSWeb.Models.RequestModels
{
    public class FilterCriterion
    {
        public string FilterCategoryKey { get; set; }
        public string FilterValue { get; set; }
        public bool IsPrimaryFilter { get; set; }
        public bool IsPrimarySubFilter { get; set; }
        public string PrimaryFilterKey { get; set; }
        public string FilterCategoryDisplayName { get; set; }
        public bool IsGroupAnd { get; set; }
    }
}