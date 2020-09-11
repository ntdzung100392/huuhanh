using DuluxGroup.Integration.Mailchimp;
using Umbraco.Core.Composing;

namespace HHCoApps.CMSWeb.Composers.Integrations
{
    public class MailchimpIntegrationComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Register<IMailchimpClient, MailchimpClient>();
            composition.Register<IMailchimpService, MailchimpService>();
        }
    }
}