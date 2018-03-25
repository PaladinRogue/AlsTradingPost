using Authentication.Domain.IdentityServices.Models;
using Common.Domain.Interfaces;

namespace Authentication.Domain.IdentityServices.Interfaces
{
    public interface IIdentityCommandService : ICreateCommandService<CreateIdentityDdto, IdentityProjection>, IUpdateCommandService<UpdateIdentityDdto, IdentityProjection>
    {
    }
}
