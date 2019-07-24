using System.Linq;
using System.Threading.Tasks;
using Authentication.Domain.Identities;
using Authentication.Domain.Identities.Queries;
using Microsoft.EntityFrameworkCore;

namespace Authtentication.Persistence.Identities
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