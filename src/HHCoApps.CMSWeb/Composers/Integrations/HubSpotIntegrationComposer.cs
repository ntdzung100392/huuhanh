using DuluxGroup.Integrations.HubSpot.Apis.v1;
using DuluxGroup.Integrations.HubSpot.Apis.v3;
using DuluxGroup.Integrations.HubSpot.Services;
using Umbraco.Core.Composing;

namespace HHCoApps.CMSWeb.Composers.Integrations
{
    public class HubSpotIntegrationComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Register<IContactApi, ContactApi>();
            composition.Register<IContactService, ContactService>();
            composition.Register<IAuthenticationApi, AuthenticationApi>();
            composition.Register<IAuthenticationService, AuthenticationService>();            
            composition.Register<ITicketApi, TicketApi>();
            composition.Register<ITicketService, TicketService>();
            composition.Register<IHubspotService, HubspotService>();
            composition.Register<IAssociationsApi, AssociationsApi>();
        }
    }
}