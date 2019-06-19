namespace HHCoApps.CMSWeb.Services.Models
{
    public class ButtonBlock : BaseBlock
    {
        public ButtonData Data { get; set; }
    }

    public class ButtonData
    {
        public string Name { get; set; }
        public string Action { get; set; }
    }
}