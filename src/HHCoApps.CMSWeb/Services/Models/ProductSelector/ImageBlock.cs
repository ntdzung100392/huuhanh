namespace HHCoApps.CMSWeb.Services.Models
{
    public class ImageBlock : BaseBlock, IConditionBlock
    {
        public ImageData Data { get; set; }
        public string ConditionKey { get; set; }
    }

    public class ImageData
    {
        public ImageDataItem[] Images { get; set; }
    }

    public class ImageDataItem
    {
        public string Id { get; set; }
        public string ImageUrl { get; set; }
        public string HoverImageUrl { get; set; }
        public string Tooltip { get; set; }
        public string ConditionValue { get; set; }
        public string Title { get; set; }
    }
}