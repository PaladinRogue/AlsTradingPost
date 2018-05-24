using Authentication.Domain.Models;
using Common.Domain.Services.Command;

namespace Authentication.Domain.ApplicationServices.Interfaces
{
    public interface IApplicationCommandService : ICreateCommandService<Application>, IUpdateCommandService<Application>
    {
    }
}
