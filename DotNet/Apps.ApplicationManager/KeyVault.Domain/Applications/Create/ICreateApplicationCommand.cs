using System.Threading.Tasks;

namespace KeyVault.Domain.Applications.Create
{
    public interface ICreateApplicationCommand
    {
        Task<Application> ExecuteAsync(CreateApplicationCommandDdto createApplicationCommandDdto);
    }
}