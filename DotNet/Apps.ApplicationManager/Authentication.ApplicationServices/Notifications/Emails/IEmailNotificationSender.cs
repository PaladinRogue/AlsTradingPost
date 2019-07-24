using System.Threading.Tasks;

namespace Authentication.ApplicationServices.Notifications.Emails
{
    public interface IEmailNotificationSender
    {
        Task SendAsync(SendEmailNotificationAdto sendEmailNotificationAdto);
    }
}