using Common.Authentication.Domain.SessionDomain.Models;
using Common.Domain.Services.Interfaces;

namespace Common.Authentication.Domain.SessionDomain.Interfaces
{
    public interface ISessionQueryService : IGetByIdService<SessionProjection>
    {
    }
}