using AutoMapper;
using WareHouseApps.Helper;

namespace WareHouseApps
{
    public partial class Report : BaseMethod
    {
        public Report(IMapper mapper) : base(mapper)
        {
            InitializeComponent();
        }
    }
}
