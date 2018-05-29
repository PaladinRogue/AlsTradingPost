using System;
using System.Collections.Generic;
using AlsTradingPost.Domain.Models.Interfaces;

namespace AlsTradingPost.Domain.PersonaDomain.Interfaces
{
    public interface IPersonaQueryService
    {
        IEnumerable<IPersona> GetUserPersonas(Guid userid);
    }
}
