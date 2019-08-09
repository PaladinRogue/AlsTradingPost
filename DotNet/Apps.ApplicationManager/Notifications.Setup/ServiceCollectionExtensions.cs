using Common.Api.Routing;
using Common.Application.Transactions;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Application.Emails;
using Notifications.Application.Emails.Send;

namespace Notifications.Setup
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