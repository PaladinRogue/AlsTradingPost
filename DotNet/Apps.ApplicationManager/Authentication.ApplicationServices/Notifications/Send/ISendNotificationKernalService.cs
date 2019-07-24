using System.Threading.Tasks;

namespace Authentication.ApplicationServices.Notifications.Send
{
    public interface ISendNotificationKernalService
    {
        Task SendAsync(SendNotificationAdto sendNotificationAdto);
    }
}