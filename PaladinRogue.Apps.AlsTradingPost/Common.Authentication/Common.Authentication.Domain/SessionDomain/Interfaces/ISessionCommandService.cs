using Common.Authentication.Domain.Models;
using Common.Domain.Services.Command;

namespace Common.Authentication.Domain.SessionDomain.Interfaces
{
    public interface ISessionCommandService : ICreateCommandService<Session>, IUpdateCommandService<Session>
    {
    }
}