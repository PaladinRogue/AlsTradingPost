using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PaladinRogue.Authentication.Domain.Identities;
using PaladinRogue.Authentication.Domain.Identities.Queries;

namespace PaladinRogue.Authentication.Persistence.Identities
{
    public class GetIdentityByForgotPasswordTokenQuery : IGetIdentityByForgotPasswordTokenQuery
    {
        private readonly AuthenticationDbContext _authenticationDbContext;

        public GetIdentityByForgotPasswordTokenQuery(AuthenticationDbContext authenticationDbContext)
        {
            _authenticationDbContext = authenticationDbContext;
        }

        public Task<Identity> RunAsync(string token)
        {
            return _authenticationDbContext.Identities
                .SingleOrDefaultAsync(i => i.AuthenticationIdentities
                    .OfType<TwoFactorAuthenticationIdentity>().Any(p =>
                        p.TwoFactorAuthenticationType == TwoFactorAuthenticationType.ForgotPassword
                        && p.Token == token));
        }
    }
}