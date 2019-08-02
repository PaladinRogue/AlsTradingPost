using System.Threading.Tasks;

namespace Notifications.ApplicationServices.Emails.Send
{
    public interface ISendEmailNotificationKernalService
    {
        Task ExecuteAsync(SendEmailNotificationAdto sendEmailNotificationAdto);
    }
}