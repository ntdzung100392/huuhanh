using HHCoApps.Repository;
using HHCoApps.Services;
using HHCoApps.Services.Interfaces;
using Ninject;
using System;
using System.Windows.Forms;
using System.Security.Principal;
using System.Threading;
using WareHouseApps.Helper;

namespace WareHouseApps
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            IKernel standardKernel = new StandardKernel(new NinjectBindingRepositories(), new NinjectBindingServices());
            log4net.Config.XmlConfigurator.Configure();
            new MapperInit().Init();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
            var windowsPrincipal = (WindowsPrincipal)Thread.CurrentPrincipal;
#if DEBUG
            Application.Run(
                new MainMenu(
                    standardKernel.Get<IVendorServices>(),
                    standardKernel.Get<ICategoryServices>(),
                    standardKernel.Get<IProductServices>()
                    ));
#else
            Application.Run(new Login(standardKernel.Get<IUserServices>()));
#endif
        }
    }
}
