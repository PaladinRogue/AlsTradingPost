namespace ApplicationManager.Domain.Applications
{
    public interface IChangeApplicationCommand
    {
        void Execute(Application application, ChangeApplicationDdto changeApplicationDdto);
    }
}
