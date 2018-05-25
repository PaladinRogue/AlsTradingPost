using Authentication.Domain.ApplicationServices.Models;
using Common.Domain.Services.Domain;

namespace Authentication.Domain.ApplicationServices.Interfaces
{
    public interface IApplicationDomainService : ICreateService<CreateApplicationDdto, ApplicationProjection>, IUpdateService<UpdateApplicationDdto, ApplicationProjection>
    {
        ApplicationProjection GetByName(string name);
    }
}
