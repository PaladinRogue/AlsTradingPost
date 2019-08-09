using System.Threading.Tasks;

namespace Notifications.Application.Emails
{
    public interface IEmailNotificationSender
    {
        Task SendAsync(SendEmailNotificationAdto sendEmailNotificationAdto);
    }
}