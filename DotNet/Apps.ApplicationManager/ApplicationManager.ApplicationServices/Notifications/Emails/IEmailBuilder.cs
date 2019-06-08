namespace ApplicationManager.ApplicationServices.Notifications.Emails
{
    public interface IEmailBuilder
    {
        EmailAdto Build(BuildEmailAdto buildEmailAdto);
    }
}