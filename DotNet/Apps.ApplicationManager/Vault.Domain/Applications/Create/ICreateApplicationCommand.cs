using System.Threading.Tasks;

namespace Vault.Domain.Applications.Create
{
    public interface ICreateApplicationCommand
    {
        Task<Application> ExecuteAsync(CreateApplicationCommandDdto createApplicationCommandDdto);
    }
}