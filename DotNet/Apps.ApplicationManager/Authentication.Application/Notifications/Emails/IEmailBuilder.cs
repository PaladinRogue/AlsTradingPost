namespace Authentication.Application.Notifications.Emails
{
    public interface IEmailBuilder
    {
        EmailAdto Build(BuildEmailAdto buildEmailAdto);
    }
}