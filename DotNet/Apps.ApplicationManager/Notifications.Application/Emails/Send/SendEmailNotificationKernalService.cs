using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PaladinRogue.Library.Core.Application.Transactions;

namespace PaladinRogue.Notifications.Application.Emails.Send
{
    public class SendEmailNotificationKernalService : ISendEmailNotificationKernalService
    {
        private readonly IEmailNotificationSender _emailNotificationSender;

        private readonly ITransactionManager _transactionManager;

        private readonly ILogger<SendEmailNotificationKernalService> _logger;

        public SendEmailNotificationKernalService(
            IEmailNotificationSender emailNotificationSender,
            ITransactionManager transactionManager,
            ILogger<SendEmailNotificationKernalService> logger)
        {
            _emailNotificationSender = emailNotificationSender;
            _transactionManager = transactionManager;
            _logger = logger;
        }

        public async Task ExecuteAsync(SendEmailNotificationAdto sendEmailNotificationAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    await _emailNotificationSender.SendAsync(sendEmailNotificationAdto);

                    transaction.Commit();
                }
                catch (Exception e)
                {
                    _logger.LogCritical("Unable to send email", sendEmailNotificationAdto, e);
                }
            }
        }
    }
}