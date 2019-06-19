using HHCoApps.CMSWeb.Composers.Indexing.Creators;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace HHCoApps.CMSWeb.Composers.Indexing
{
    public class CustomIndexComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Components().Append<CustomIndexComponent>();
            composition.RegisterUnique<ContentIndexCreator>();
            composition.RegisterUnique<QuestionIndexCreator>();
        }
    }
}