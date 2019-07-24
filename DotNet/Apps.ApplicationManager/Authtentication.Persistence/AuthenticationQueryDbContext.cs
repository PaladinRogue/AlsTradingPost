using Authentication.Domain.Identities.Projections;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityFramework.Infrastructure.Extensions;

namespace Authtentication.Persistence
{
    public partial class AuthenticationDbContext
    {
        private static void ConfigureQueryTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Query<TwoFactorAuthenticationIdentityProjection>()
                .ProtectSensitiveInformation()
                .ToView("TwoFactorAuthenticationIdentityProjections");
        }
    }
}