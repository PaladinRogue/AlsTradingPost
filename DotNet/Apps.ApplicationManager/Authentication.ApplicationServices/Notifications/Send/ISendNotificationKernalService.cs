using System.Threading.Tasks;
using Notifications.ApplicationServices.Emails;

namespace Authentication.ApplicationServices.Notifications.Send
{
    public interface ISendNotificationKernalService
    {
        Task SendAsync(SendNotificationAdto sendNotificationAdto);
    }
}