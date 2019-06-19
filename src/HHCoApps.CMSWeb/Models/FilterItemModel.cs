namespace HHCoApps.CMSWeb.Models
{
    public class FilterItemModel
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public long ItemsCount { get; set; }
        public bool IsPrimarySubFilter { get; set; }
        public string PrimaryFilterKey { get; set; }
    }
}