using Microsoft.Extensions.DependencyInjection;
using PaladinRogue.Library.Core.Setup.Infrastructure.Routing;
using PaladinRogue.Notifications.Application.Emails;
using PaladinRogue.Notifications.Application.Emails.Send;

namespace PaladinRogue.Notifications.Setup
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseDefaultRouting(this IServiceCollection services)
        {
            return services
                .AddSingleton<IRouteProvider<bool>, DefaultRouteProvider>();
        }
        public static IServiceCollection UseEmailNotifications(this IServiceCollection services)
        {
            return services
                .AddScoped<ISendEmailNotificationKernalService, SendEmailNotificationKernalService>()
                .AddScoped<IEmailNotificationSender, LocalDevelopmentEmailNotificationSender>();
        }
    }
}