using System;
using System.Collections.Generic;
using AlsTradingPost.Domain.PersonaDomain.Models;

namespace AlsTradingPost.Domain.PersonaDomain.Interfaces
{
    public interface IPersonaDomainService
    {
        IEnumerable<PersonaProjection> GetUserPersonas(Guid userid);
    }
}
