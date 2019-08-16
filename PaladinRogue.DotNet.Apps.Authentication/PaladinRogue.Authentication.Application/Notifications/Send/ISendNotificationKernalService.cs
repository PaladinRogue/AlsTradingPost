using System.Threading.Tasks;

namespace PaladinRogue.Authentication.Application.Notifications.Send
{
    public interface ISendNotificationKernalService
    {
        Task SendAsync(SendNotificationAdto sendNotificationAdto);
    }
}