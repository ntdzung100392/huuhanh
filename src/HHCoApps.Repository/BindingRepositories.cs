using HHCoApps.Repository.Implementations;
using HHCoApps.Repository.Interfaces;
using Ninject.Modules;

namespace HHCoApps.Repository
{
    public class BindingRepositories : NinjectModule
    {
        public override void Load()
        {
            Bind<IVendorRepository>().To<VendorRepository>();
            Bind<IProductRepository>().To<ProductRepository>();
            Bind<IUserRepository>().To<UserRepository>();
            Bind<ICategoryRepository>().To<CategoryRepository>();
            Bind<ICurrencyRepository>().To<CurrencyRepository>();
            Bind<IContactRepository>().To<ContactRepository>();
        }
    }
}
