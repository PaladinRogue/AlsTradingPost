﻿using Authentication.Domain.ApplicationServices.Models;
using Common.Domain.Interfaces;

namespace Authentication.Domain.ApplicationServices.Interfaces
{
    public interface IApplicationQueryService : IQueryService<ApplicationProjection>
    {
    }
}
