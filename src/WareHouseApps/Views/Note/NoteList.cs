using AutoMapper;
using WareHouseApps.Helper;

namespace WareHouseApps
{
    public partial class NoteList : BaseMethod
    {
        public NoteList(IMapper mapper) : base(mapper)
        {
            InitializeComponent();
        }
    }
}
