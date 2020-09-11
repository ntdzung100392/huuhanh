using AutoMapper;
using WareHouseApps.Helper;

namespace WareHouseApps
{
    public partial class ExportProduct : BaseMethod
    {
        public ExportProduct(IMapper mapper) : base(mapper)
        {
            InitializeComponent();
        }
    }
}
