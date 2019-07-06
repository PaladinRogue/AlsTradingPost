using System.Threading.Tasks;

namespace ApplicationManager.ApplicationServices.Notifications.Send
{
    public interface ISendNotificationKernalService
    {
        Task SendAsync(SendNotificationAdto sendNotificationAdto);
    }
}