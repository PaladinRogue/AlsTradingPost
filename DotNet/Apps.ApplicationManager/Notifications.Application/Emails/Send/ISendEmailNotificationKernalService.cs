using System.Threading.Tasks;

namespace PaladinRogue.Notifications.Application.Emails.Send
{
    public interface ISendEmailNotificationKernalService
    {
        Task ExecuteAsync(SendEmailNotificationAdto sendEmailNotificationAdto);
    }
}