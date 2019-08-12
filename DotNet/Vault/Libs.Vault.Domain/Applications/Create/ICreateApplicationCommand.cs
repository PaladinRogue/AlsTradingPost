using System.Threading.Tasks;

namespace PaladinRogue.Library.Vault.Domain.Applications.Create
{
    public interface ICreateApplicationCommand
    {
        Task<Application> ExecuteAsync(CreateApplicationCommandDdto createApplicationCommandDdto);
    }
}