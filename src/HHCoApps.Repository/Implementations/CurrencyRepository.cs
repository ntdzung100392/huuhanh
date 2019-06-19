using HHCoApps.Core;
using System.Collections.Generic;
using System.Linq;

namespace HHCoApps.Repository.Implementations
{
    internal class CurrencyRepository : ICurrencyRepository
    {
        public CurrencyRepository() { }

        public IEnumerable<Currency> GetCurrencies()
        {
            using (var context = new HuuHanhEntities())
            {
                return GetCurrencies(context.Currencies);
            }
        }

        internal IEnumerable<Currency> GetCurrencies(IQueryable<Currency> currencies)
        {
            return currencies.Where(c => c.IsActive && !c.IsDeleted);
        }
    }
}
