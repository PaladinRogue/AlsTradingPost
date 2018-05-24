using Common.Authentication.Domain.Models;
using Common.Domain.Services.Domain;

namespace Common.Authentication.Domain.SessionDomain.Interfaces
{
    public interface ISessionQueryService : IGetByIdService<Session>
    {
    }
}