using Authentication.Domain.Models;
using Common.Domain.Services.Query;

namespace Authentication.Domain.ApplicationServices.Interfaces
{
    public interface IApplicationQueryService : IGetByIdQueryService<Application>
    {
        Application GetByName(string applicationName);
    }
}
