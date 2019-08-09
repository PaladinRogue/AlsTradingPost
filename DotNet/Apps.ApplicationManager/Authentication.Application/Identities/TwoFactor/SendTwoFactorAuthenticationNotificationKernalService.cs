using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Application.Transactions;
using Messaging.Messages;
using Messaging.Setup.Infrastructure;

namespace Authentication.Application.Identities.TwoFactor
{
    public class SendTwoFactorAuthenticationNotificationKernalService : ISendTwoFactorAuthenticationNotificationKernalService
    {
        private readonly ITransactionManager _transactionManager;

        public SendTwoFactorAuthenticationNotificationKernalService(
            ITransactionManager transactionManager)
        {
            _transactionManager = transactionManager;
        }

        public async Task SendAsync(SendTwoFactorAuthenticationNotificationAdto sendTwoFactorAuthenticationNotificationAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                await Message.SendAsync(SendNotificationMessage.Create(
                    TwoFactorAuthenticationNotificationMap.ForType(sendTwoFactorAuthenticationNotificationAdto.TwoFactorAuthenticationType),
                    sendTwoFactorAuthenticationNotificationAdto.IdentityId,
                    new Dictionary<string, string>
                    {
                        {
                            nameof(sendTwoFactorAuthenticationNotificationAdto.Token),
                            sendTwoFactorAuthenticationNotificationAdto.Token
                        }
                    }));

                transaction.Commit();
            }
        }
    }
}