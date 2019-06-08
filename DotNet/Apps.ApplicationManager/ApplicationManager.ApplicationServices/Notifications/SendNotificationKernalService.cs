using System;
using System.Collections.Generic;
using ApplicationManager.ApplicationServices.Notifications.Audience;
using ApplicationManager.ApplicationServices.Notifications.Emails;
using ApplicationManager.Domain.NotificationTypes;
using Common.Application.Exceptions;
using Common.Application.Transactions;
using Common.Domain.Persistence;
using Microsoft.Extensions.Logging;

namespace ApplicationManager.ApplicationServices.Notifications
{
    public class SendNotificationKernalService : ISendNotificationKernalService
    {
        private readonly ILogger<SendNotificationKernalService> _logger;

        private readonly IQueryRepository<NotificationType> _queryRepository;

        private readonly IChannelAudienceResolverProvider _channelAudienceResolverProvider;
        
        private readonly ITransactionManager _transactionManager;
        
        private readonly IEmailNotificationSender _emailNotificationSender;
        
        private readonly IEmailBuilder _emailBuilder;

        public SendNotificationKernalService(
            ILogger<SendNotificationKernalService> logger,
            IQueryRepository<NotificationType> queryRepository,
            IChannelAudienceResolverProvider channelAudienceResolverProvider,
            ITransactionManager transactionManager,
            IEmailNotificationSender emailNotificationSender,
            IEmailBuilder emailBuilder)
        {
            _logger = logger;
            _queryRepository = queryRepository;
            _channelAudienceResolverProvider = channelAudienceResolverProvider;
            _transactionManager = transactionManager;
            _emailNotificationSender = emailNotificationSender;
            _emailBuilder = emailBuilder;
        }

        public void Send(SendNotificationAdto sendNotificationAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    NotificationType notificationType =
                        _queryRepository.GetSingle(n => n.Type == sendNotificationAdto.NotificationType);
                    if (notificationType != null)
                    {
                        foreach (NotificationTypeChannel notificationTypeChannel in notificationType
                            .NotificationTypeChannels)
                        {
                            IChannelAudienceResolver channelAudienceResolver =
                                _channelAudienceResolverProvider.GetByType(notificationTypeChannel.ChannelType,
                                    sendNotificationAdto.NotificationType);

                            IEnumerable<string> emailAddresses =
                                channelAudienceResolver.GetAudience(sendNotificationAdto.IdentityId);

                            switch (notificationTypeChannel.ChannelType)
                            {
                                case ChannelType.Email:
                                    foreach (string emailAddress in emailAddresses)
                                    {
                                        EmailChannelTemplate emailChannelTemplate =
                                            (notificationTypeChannel.NotificationChannelTemplate as
                                                EmailChannelTemplate);

                                        EmailAdto emailAdto = _emailBuilder.Build(new BuildEmailAdto
                                        {
                                            EmailTemplate = emailChannelTemplate?.Template,
                                            Subject = emailChannelTemplate?.Subject,
                                            PropertyBag = sendNotificationAdto.PropertyBag
                                        });

                                        try
                                        {
                                            _emailNotificationSender.Send(new SendEmailNotificationAdto
                                            {
                                                From = "noreply@paladin-rogue.com",
                                                Recipients = new List<string> {emailAddress},
                                                HtmlBody = emailAdto.HtmlBody,
                                                Subject = emailAdto.Subject
                                            });
                                        }
                                        catch (Exception e)
                                        {
                                            _logger.LogCritical("Unable to send email", emailAdto, e);
                                        }
                                    }

                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        }
                    }

                    transaction.Commit();
                }
                catch (BusinessApplicationException e)
                {
                    _logger.LogDebug(e.Message);
                }
            }
        }
    }
}