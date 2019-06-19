using HHCoApps.Repository;
using HHCoApps.Services.Interfaces;

namespace HHCoApps.Services.Implementation
{
    internal class CurrencyServices : ICurrencyServices
    {
        private readonly ICurrencyRepository _currencyRepository;

        public CurrencyServices(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }
    }
}
