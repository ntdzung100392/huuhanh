namespace HHCoApps.CMSWeb.Models.Enums
{
    public class Icon : Headspring.Enumeration<Icon, string>
    {
        public static Icon SocialFacebook = new Icon("facebook", "Social Facebook");
        public static Icon SocialInstagram = new Icon("instagram", "Social Instagram");
        public static Icon SocialPinterest = new Icon("pinterest", "Social Pinterest");
        public static Icon SocialYoutube = new Icon("youtube", "Social Youtube");

        public static Icon Can = new Icon("can", "Can");
        public static Icon Bucket = new Icon("bucket", "Bucket");
        public static Icon Brush = new Icon("brush", "Brush");
        public static Icon Spray = new Icon("spray", "Spray");

        public Icon(string value, string displayName) : base(value, displayName)
        {
        }
    }
}