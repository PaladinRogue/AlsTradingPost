using System.Collections.Generic;
using Common.Application.Transactions;
using Common.Messaging.Infrastructure;
using Common.Messaging.Messages;

namespace ApplicationManager.ApplicationServices.Identities.TwoFactor
{
    public class SendTwoFactorAuthenticationNotificationKernalService : ISendTwoFactorAuthenticationNotificationKernalService
    {
        private readonly ITransactionManager _transactionManager;

        public SendTwoFactorAuthenticationNotificationKernalService(
            ITransactionManager transactionManager)
        {
            _transactionManager = transactionManager;
        }

        public void Send(SendTwoFactorAuthenticationNotificationAdto sendTwoFactorAuthenticationNotificationAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                Message.Send(SendNotificationMessage.Create(
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