namespace ApplicationManager.ApplicationServices.Notifications.Emails
{
    public interface IEmailNotificationSender
    {
        void Send(SendEmailNotificationAdto sendEmailNotificationAdto);
    }
}