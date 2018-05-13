using System;
using AlsTradingPost.Domain.UserDomain.Models;
using AlsTradingPost.Resources;

namespace AlsTradingPost.Domain.UserDomain.Interfaces
{
    public interface IUserDomainService
    {
        AuthenticatedUserProjection Login(LoginDdto loginDdto);
        
        PersonaFlags GetUserPersonaFlags(Guid userId);
    }
}