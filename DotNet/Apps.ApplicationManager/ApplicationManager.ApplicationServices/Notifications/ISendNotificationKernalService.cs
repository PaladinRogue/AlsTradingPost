namespace ApplicationManager.ApplicationServices.Notifications
{
    public interface ISendNotificationKernalService
    {
        void Send(SendNotificationAdto sendNotificationAdto);
    }
}