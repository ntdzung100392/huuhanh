using HHCoApps.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WareHouseApps.Helper;

namespace WareHouseApps
{
    public partial class Login : BaseMethod
    {
        private readonly IUserServices userServices;
        public Login(IUserServices userServices)
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
