using HHCoApps.Services.Implementation;
using HHCoApps.Services.Interfaces;
using Ninject.Modules;

namespace HHCoApps.Services
{
    public class NinjectBindingServices : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserServices>().To<UserServices>();
            Bind<IProductServices>().To<ProductServices>();
            Bind<IVendorServices>().To<VendorServices>();
            Bind<ICategoryServices>().To<CategoryServices>();
            Bind<ICurrencyServices>().To<CurrencyServices>();
            Bind<IContactServices>().To<ContactServices>();
        }
    }
}
