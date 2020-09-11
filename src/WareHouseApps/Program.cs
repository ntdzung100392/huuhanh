using AutoMapper;
using HHCoApps.Repository;
using HHCoApps.Services;
using HHCoApps.Services.Interfaces;
using Ninject;
using Ninject.Modules;
using System;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;
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
            var modules = new INinjectModule[] { new RepositoriesComposer(), new ServicesComposer() };
            IKernel standardKernel = new StandardKernel(modules);
            new MapperInit().Load();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
            var windowsPrincipal = (WindowsPrincipal)Thread.CurrentPrincipal;
#if DEBUG
            Application.Run(
                new MainMenu(
                    standardKernel.Get<IVendorServices>(),
                    standardKernel.Get<ICategoryServices>(),
                    standardKernel.Get<IProductServices>(),
                    standardKernel.Get<IMapper>()
                    ));
#else
            Application.Run(new Login(standardKernel.Get<IUserServices>()));
#endif
        }
    }
}
