using Microsoft.EntityFrameworkCore;
using PaladinRogue.Authentication.Domain.Identities.Projections;
using PaladinRogue.Libray.Persistence.EntityFramework.Infrastructure.Extensions;

namespace PaladinRogue.Authentication.Persistence
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