using Authentication.Application.Identity.Models;

namespace Authentication.Application.Identity.Interfaces
{
    public interface IIdentityApplicationService
    {
        IdentityAdto Get(GetIdentityAdto identity);
        IdentityAdto Update(UpdateIdentityAdto identity);
    }
}
