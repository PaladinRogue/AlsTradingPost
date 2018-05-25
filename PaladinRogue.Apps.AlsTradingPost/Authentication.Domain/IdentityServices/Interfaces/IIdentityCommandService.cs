using Authentication.Domain.Models;
using Common.Domain.Services.Command;

namespace Authentication.Domain.IdentityServices.Interfaces
{
    public interface IIdentityCommandService : ICreateCommandService<Identity>
    {
    }
}
