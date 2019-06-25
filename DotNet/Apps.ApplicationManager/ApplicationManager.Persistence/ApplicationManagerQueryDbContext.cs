using ApplicationManager.Domain.Identities.Projections;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityFramework.Infrastructure.Extensions;

namespace ApplicationManager.Persistence
{
    public partial class ApplicationManagerDbContext
    {
        private static void ConfigureQueryTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Query<TwoFactorAuthenticationIdentityProjection>()
                .ProtectSensitiveInformation()
                .ToView("TwoFactorAuthenticationIdentityProjection");
        }
    }
}
