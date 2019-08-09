using System.Threading.Tasks;
using Notifications.ApplicationServices.Emails;

namespace Authentication.Application.Notifications.Send
{
    public interface ISendNotificationKernalService
    {
        Task SendAsync(SendNotificationAdto sendNotificationAdto);
    }
}