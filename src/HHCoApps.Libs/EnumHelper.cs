using System.ComponentModel;

namespace HHCoApps.Libs
{
    public enum Sex
    {
        [Description("Nam")]
        Nam = 0,
        [Description("Nữ")]
        Nữ = 1
    }

    public enum ProductStatus
    {
        [Description("Nhập Kho/Tồn Kho")]
        IN_STOCK,
        [Description("Hết Hàng")]
        OUT_OF_STOCK,
        [Description("Hết Hạn Bảo Hành")]
        EXPIRED,
        [Description("Ký Gửi")]
        FOR_TRADE,
        [Description("Dự Trữ Bảo Hành")]
        IN_RESERVED
    }
}
