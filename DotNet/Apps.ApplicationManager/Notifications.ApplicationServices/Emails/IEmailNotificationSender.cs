using System.Threading.Tasks;

namespace Notifications.ApplicationServices.Emails
{
    public interface IEmailNotificationSender
    {
        Task SendAsync(SendEmailNotificationAdto sendEmailNotificationAdto);
    }
}