using System.Threading.Tasks;

namespace PaladinRogue.Libray.Vault.Domain.Applications.Create
{
    public interface ICreateApplicationCommand
    {
        Task<Application> ExecuteAsync(CreateApplicationCommandDdto createApplicationCommandDdto);
    }
}