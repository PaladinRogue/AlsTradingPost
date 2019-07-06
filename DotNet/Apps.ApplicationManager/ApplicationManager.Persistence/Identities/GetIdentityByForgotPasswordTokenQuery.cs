using System.Linq;
using System.Threading.Tasks;
using ApplicationManager.Domain.Identities;
using ApplicationManager.Domain.Identities.Queries;
using Microsoft.EntityFrameworkCore;

namespace ApplicationManager.Persistence.Identities
{
    public class GetIdentityByForgotPasswordTokenQuery : IGetIdentityByForgotPasswordTokenQuery
    {
        private readonly ApplicationManagerDbContext _applicationManagerDbContext;

        public GetIdentityByForgotPasswordTokenQuery(ApplicationManagerDbContext applicationManagerDbContext)
        {
            _applicationManagerDbContext = applicationManagerDbContext;
        }

        public Task<Identity> RunAsync(string token)
        {
            return _applicationManagerDbContext.Identities
                .SingleOrDefaultAsync(i => i.AuthenticationIdentities
                    .OfType<TwoFactorAuthenticationIdentity>().Any(p =>
                        p.TwoFactorAuthenticationType == TwoFactorAuthenticationType.ForgotPassword
                        && p.Token == token));
        }
    }
}