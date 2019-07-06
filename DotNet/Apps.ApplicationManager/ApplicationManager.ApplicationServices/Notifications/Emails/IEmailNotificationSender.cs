using System.Threading.Tasks;

namespace ApplicationManager.ApplicationServices.Notifications.Emails
{
    public interface IEmailNotificationSender
    {
        Task SendAsync(SendEmailNotificationAdto sendEmailNotificationAdto);
    }
}