namespace ApplicationManager.Domain.Applications.Change
{
    public interface IChangeApplicationCommand
    {
        void Execute(Application application, ChangeApplicationDdto changeApplicationDdto);
    }
}
