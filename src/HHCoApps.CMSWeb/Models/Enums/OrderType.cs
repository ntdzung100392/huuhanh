using HHCoApps.CMSWeb.Headspring;

namespace HHCoApps.CMSWeb.Models.Enums
{
    public class OrderType : Enumeration<OrderType, string>
    {
        public static OrderType Ascending = new OrderType("Ascending", "Ascending");
        public static OrderType Descending = new OrderType("Descending", "Descending");

        public OrderType(string value, string displayName) : base(value, displayName)
        {
        }
    }
}