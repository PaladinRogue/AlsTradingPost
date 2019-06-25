using System.Linq;
using ApplicationManager.Domain;
using ApplicationManager.Domain.Identities;
using ApplicationManager.Domain.Identities.CheckPassword;
using ApplicationManager.Domain.Identities.Queries;
using Common.Domain.Models.DataProtection;

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