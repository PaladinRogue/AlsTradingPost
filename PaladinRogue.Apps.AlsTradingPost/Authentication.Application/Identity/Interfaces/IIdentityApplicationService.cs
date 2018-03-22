using System;
using Authentication.Application.Identity.Models;

namespace Authentication.Application.Identity.Interfaces
{
    public interface IIdentityApplicationService
    {
        IdentityAdto Get(Guid id);
        IdentityAdto Create(CreateIdentityAdto identity);
        IdentityAdto Update(UpdateIdentityAdto identity);
    }
}
