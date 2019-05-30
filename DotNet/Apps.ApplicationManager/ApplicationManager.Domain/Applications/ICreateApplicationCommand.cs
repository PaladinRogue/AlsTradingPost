namespace ApplicationManager.Domain.Applications
{
    public interface ICreateApplicationCommand
    {
        Application Execute(CreateApplicationDdto createApplicationDdto);
    }
}
