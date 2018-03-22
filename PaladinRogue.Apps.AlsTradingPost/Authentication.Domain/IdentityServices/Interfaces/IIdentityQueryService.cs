using Authentication.Domain.IdentityServices.Models;
using Common.Domain.Interfaces;

namespace Authentication.Domain.IdentityServices.Interfaces
{
    public interface IIdentityQueryService : IQueryService<IdentityProjection>
    {
    }
}
