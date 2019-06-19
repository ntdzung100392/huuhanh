using HHCoApps.Core;
using System.Collections.Generic;

namespace HHCoApps.Repository
{
    public interface ICurrencyRepository
    {
        IEnumerable<Currency> GetCurrencies();
    }
}
