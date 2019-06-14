namespace ApplicationManager.ApplicationServices.Notifications.Send
{
    public interface ISendNotificationKernalService
    {
        void Send(SendNotificationAdto sendNotificationAdto);
    }
}