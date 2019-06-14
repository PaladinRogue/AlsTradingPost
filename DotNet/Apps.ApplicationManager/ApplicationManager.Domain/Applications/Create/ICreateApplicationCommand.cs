namespace ApplicationManager.Domain.Applications.Create
{
    public interface ICreateApplicationCommand
    {
        Application Execute(CreateApplicationDdto createApplicationDdto);
    }
}
