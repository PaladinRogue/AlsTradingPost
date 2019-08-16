using System.Threading.Tasks;

namespace PaladinRogue.Notifications.Application.Emails
{
    public interface IEmailNotificationSender
    {
        Task SendAsync(SendEmailNotificationAdto sendEmailNotificationAdto);
    }
}