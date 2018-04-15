using Authentication.Domain.ApplicationServices.Models;
using Common.Domain.Services;
using Common.Domain.Services.Interfaces;

namespace Authentication.Domain.ApplicationServices.Interfaces
{
    public interface IApplicationQueryService : IQueryService<ApplicationProjection>
    {
        ApplicationProjection GetByName(string applicationName);
    }
}
