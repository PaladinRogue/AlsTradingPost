using System.Linq;
using ApplicationManager.Domain.Identities;
using ApplicationManager.Domain.Identities.Queries;

namespace ApplicationManager.Persistence.Identities
{
    public class GetIdentityByForgotPasswordTokenQuery : IGetIdentityByForgotPasswordTokenQuery
    {
        private readonly ApplicationManagerDbContext _applicationManagerDbContext;

        public GetIdentityByForgotPasswordTokenQuery(ApplicationManagerDbContext applicationManagerDbContext)
        {
            _applicationManagerDbContext = applicationManagerDbContext;
        }

        public Identity Run(string token)
        {
            return _applicationManagerDbContext.Identities
                .SingleOrDefault(i => i.AuthenticationIdentities
                    .OfType<TwoFactorAuthenticationIdentity>().Any(p =>
                        p.TwoFactorAuthenticationType == TwoFactorAuthenticationType.ForgotPassword
                        && p.Token == token));
        }
    }
}