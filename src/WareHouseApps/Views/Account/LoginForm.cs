using AutoMapper;
using HHCoApps.Services.Interfaces;
using System;
using WareHouseApps.Helper;

namespace WareHouseApps
{
    public partial class Login : BaseMethod
    {
        private readonly IUserServices userServices;
        public Login(IUserServices userServices, IMapper mapper) : base(mapper)
        {
            this.userServices = userServices;
            InitializeComponent();
            this.CenterToScreen();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (this.Validate())
            {

            }
        }
    }
}
