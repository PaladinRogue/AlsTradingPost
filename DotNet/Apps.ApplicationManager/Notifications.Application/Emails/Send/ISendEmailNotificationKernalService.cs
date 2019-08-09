using System.Threading.Tasks;

namespace Notifications.Application.Emails.Send
{
    public interface ISendEmailNotificationKernalService
    {
        Task ExecuteAsync(SendEmailNotificationAdto sendEmailNotificationAdto);
    }
}