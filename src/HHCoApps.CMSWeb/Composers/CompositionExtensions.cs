using Umbraco.Core.Composing;

namespace HHCoApps.CMSWeb.Composers
{
    public static class CompositionExtensions
    {
        public static void Register<TService, TImplementation>(this Composition composition, Lifetime lifetime = Lifetime.Transient)
        {
            composition.Register(typeof(TService), typeof(TImplementation), lifetime);
        }

        public static void Register<TService>(this Composition composition, Lifetime lifetime = Lifetime.Transient)
        {
            composition.Register(typeof(TService), typeof(TService), lifetime);
        }
    }
}