using Authentication.Domain.ApplicationServices.Models;
using Common.Domain.Services;

namespace Authentication.Domain.ApplicationServices.Interfaces
{
    public interface IApplicationCommandService : ICreateCommandService<CreateApplicationDdto, ApplicationProjection>, IUpdateCommandService<UpdateApplicationDdto, ApplicationProjection>
    {
    }
}
