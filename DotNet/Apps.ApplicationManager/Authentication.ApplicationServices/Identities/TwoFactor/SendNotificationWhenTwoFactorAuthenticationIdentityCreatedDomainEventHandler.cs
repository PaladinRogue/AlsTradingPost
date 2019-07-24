using System.Threading.Tasks;
using Authentication.Domain.Identities.Events;
using Common.Domain.DomainEvents.Interfaces;

namespace Authentication.ApplicationServices.Identities.TwoFactor
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

        public Task HandleAsync(TwoFactorAuthenticationIdentityCreatedDomainEvent domainEvent)
        {
            return _sendTwoFactorAuthenticationNotificationKernalService.SendAsync(new SendTwoFactorAuthenticationNotificationAdto
            {
                IdentityId = domainEvent.TwoFactorAuthenticationIdentity.Identity.Id,
                Token = domainEvent.TwoFactorAuthenticationIdentity.Token,
                TwoFactorAuthenticationType = domainEvent.TwoFactorAuthenticationIdentity.TwoFactorAuthenticationType
            });
        }
    }
}