using System.Linq;
using ApplicationManager.Domain.Identities;
using ApplicationManager.Domain.Identities.CheckPassword;
using ApplicationManager.Domain.Identities.Queries;

namespace ApplicationManager.Persistence.Identities
{
    public class GetIdentityByIdentifierAndPasswordQuery : IGetIdentityByIdentifierAndPasswordQuery
    {
        private readonly ApplicationManagerDbContext _applicationManagerDbContext;

        private readonly ICheckPasswordCommand _checkPasswordCommand;

        public GetIdentityByIdentifierAndPasswordQuery(
            ApplicationManagerDbContext applicationManagerDbContext,
            ICheckPasswordCommand checkPasswordCommand)
        {
            _applicationManagerDbContext = applicationManagerDbContext;
            _checkPasswordCommand = checkPasswordCommand;
        }

        public Identity Run(string identifier, string password)
        {
            Identity identity = _applicationManagerDbContext.Identities
                .SingleOrDefault(i => i.AuthenticationIdentities.OfType<PasswordIdentity>().Any(p => p.Identifier == identifier));

            PasswordIdentity passwordIdentity = (PasswordIdentity) identity?.AuthenticationIdentities.Single(a => a is PasswordIdentity);

            if (passwordIdentity == null)
            {
                return null;
            }

            return _checkPasswordCommand.Execute(passwordIdentity,
                new CheckPasswordDdto
                {
                    Password = password
                })
                ? identity
                : null;
        }
    }
}