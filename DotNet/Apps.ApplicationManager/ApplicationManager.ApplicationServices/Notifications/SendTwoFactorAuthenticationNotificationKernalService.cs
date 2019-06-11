using System.Collections.Generic;
using Common.Application.Transactions;
using Common.Messaging.Infrastructure;
using Common.Messaging.Infrastructure.Interfaces;
using Common.Messaging.Message.Interfaces;
using Common.Messaging.Messages;

namespace ApplicationManager.ApplicationServices.Notifications
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
                    Domain.NotificationTypes.NotificationTypes.EmailTwoFactorAuthentication,
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