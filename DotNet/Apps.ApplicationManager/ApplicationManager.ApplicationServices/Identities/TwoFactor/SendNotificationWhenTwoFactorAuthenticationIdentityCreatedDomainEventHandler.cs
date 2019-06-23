using ApplicationManager.Domain.Identities.Events;
using Common.Domain.DomainEvents.Interfaces;

namespace ApplicationManager.ApplicationServices.Identities.TwoFactor
{
    public class SendNotificationWhenTwoFactorAuthenticationIdentityCreatedDomainEventHandler : IDomainEventHandler<TwoFactorAuthenticationIdentityCreatedDomainEvent>
    {
        private readonly ISendTwoFactorAuthenticationNotificationKernalService
            _sendTwoFactorAuthenticationNotificationKernalService;

        public SendNotificationWhenTwoFactorAuthenticationIdentityCreatedDomainEventHandler(
            ISendTwoFactorAuthenticationNotificationKernalService sendTwoFactorAuthenticationNotificationKernalService)
        {
            _sendTwoFactorAuthenticationNotificationKernalService = sendTwoFactorAuthenticationNotificationKernalService;
        }

        public void Handle(TwoFactorAuthenticationIdentityCreatedDomainEvent domainEvent)
        {
            _sendTwoFactorAuthenticationNotificationKernalService.Send(new SendTwoFactorAuthenticationNotificationAdto
            {
                IdentityId = domainEvent.TwoFactorAuthenticationIdentity.Identity.Id,
                Token = domainEvent.TwoFactorAuthenticationIdentity.Token,
                TwoFactorAuthenticationType = domainEvent.TwoFactorAuthenticationIdentity.TwoFactorAuthenticationType
            });
        }
    }
}