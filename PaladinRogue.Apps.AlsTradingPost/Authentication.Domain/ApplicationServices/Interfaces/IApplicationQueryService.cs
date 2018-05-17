using Authentication.Domain.ApplicationServices.Models;
using Common.Domain.Services.Interfaces;

namespace Authentication.Domain.ApplicationServices.Interfaces
{
    public interface IApplicationQueryService : IGetByIdService<ApplicationProjection>
    {
        ApplicationProjection GetByName(string applicationName);
    }
}
