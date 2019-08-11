using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Notifications.Messages;
using PaladinRogue.Authentication.Application.Notifications.Audiences;
using PaladinRogue.Authentication.Application.Notifications.Emails;
using PaladinRogue.Authentication.Domain.NotificationTypes;
using PaladinRogue.Libray.Core.Application.Exceptions;
using PaladinRogue.Libray.Core.Application.Transactions;
using PaladinRogue.Libray.Core.Domain.Persistence;
using PaladinRogue.Libray.Messaging.Common.Messages;

namespace PaladinRogue.Authentication.Application.Notifications.Send
{
    public class SendNotificationKernalService : ISendNotificationKernalService
    {
        private readonly ILogger<SendNotificationKernalService> _logger;

        private readonly IQueryRepository<NotificationType> _queryRepository;

        private readonly IChannelAudienceResolverProvider _channelAudienceResolverProvider;

        private readonly ITransactionManager _transactionManager;

        private readonly IEmailBuilder _emailBuilder;

        public SendNotificationKernalService(
            ILogger<SendNotificationKernalService> logger,
            IQueryRepository<NotificationType> queryRepository,
            IChannelAudienceResolverProvider channelAudienceResolverProvider,
            ITransactionManager transactionManager,
            IEmailBuilder emailBuilder)
        {
            _logger = logger;
            _queryRepository = queryRepository;
            _channelAudienceResolverProvider = channelAudienceResolverProvider;
            _transactionManager = transactionManager;
            _emailBuilder = emailBuilder;
        }

        public async Task SendAsync(SendNotificationAdto sendNotificationAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    NotificationType notificationType = await
                        _queryRepository.GetSingleAsync(n => n.Type == sendNotificationAdto.NotificationType);

                    if (notificationType != null)
                    {
                        IEnumerable<NotificationTypeChannel> notificationTypeNotificationTypeChannels = notificationType.NotificationTypeChannels;

                        foreach (NotificationTypeChannel notificationTypeChannel in notificationTypeNotificationTypeChannels)
                        {
                            IChannelAudienceResolver channelAudienceResolver =
                                _channelAudienceResolverProvider.GetByType(notificationTypeChannel.ChannelType,
                                    sendNotificationAdto.NotificationType);

                            IEnumerable<string> emailAddresses = await channelAudienceResolver.GetAudienceAsync(sendNotificationAdto.IdentityId);

                            switch (notificationTypeChannel.ChannelType)
                            {
                                case ChannelType.Email:
                                    foreach (string emailAddress in emailAddresses)
                                    {
                                        if (!(notificationTypeChannel.NotificationChannelTemplate is EmailChannelTemplate emailChannelTemplate))
                                        {
                                            throw new NullReferenceException("No template defined for channel");
                                        }

                                        EmailAdto emailAdto = _emailBuilder.Build(new BuildEmailAdto
                                        {
                                            EmailTemplate = emailChannelTemplate.Template,
                                            Subject = emailChannelTemplate.Subject,
                                            PropertyBag = sendNotificationAdto.PropertyBag
                                        });

                                        await Message.SendAsync(SendEmailNotificationMessage.Create("noreply@paladin-rogue.com", emailAddress, emailAdto.Subject, emailAdto.HtmlBody));
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