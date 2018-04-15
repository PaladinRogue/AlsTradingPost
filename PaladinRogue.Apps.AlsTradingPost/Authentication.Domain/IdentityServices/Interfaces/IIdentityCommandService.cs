using Authentication.Domain.IdentityServices.Models;
using Common.Domain.Services;
using Common.Domain.Services.Interfaces;

namespace Authentication.Domain.IdentityServices.Interfaces
{
    public interface IIdentityCommandService : ICreateCommandService<CreateIdentityDdto, IdentityProjection>, IUpdateCommandService<UpdateIdentityDdto, IdentityProjection>
    {
    }
}
