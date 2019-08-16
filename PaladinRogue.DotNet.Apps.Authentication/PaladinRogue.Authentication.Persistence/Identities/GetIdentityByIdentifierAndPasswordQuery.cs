using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PaladinRogue.Authentication.Domain.Identities;
using PaladinRogue.Authentication.Domain.Identities.CheckPassword;
using PaladinRogue.Authentication.Domain.Identities.Queries;

namespace PaladinRogue.Authentication.Persistence.Identities
{
    public class GetIdentityByIdentifierAndPasswordQuery : IGetIdentityByIdentifierAndPasswordQuery
    {
        private readonly AuthenticationDbContext _authenticationDbContext;

        private readonly ICheckPasswordCommand _checkPasswordCommand;

        public GetIdentityByIdentifierAndPasswordQuery(
            AuthenticationDbContext authenticationDbContext,
            ICheckPasswordCommand checkPasswordCommand)
        {
            _authenticationDbContext = authenticationDbContext;
            _checkPasswordCommand = checkPasswordCommand;
        }

        public async Task<Identity> RunAsync(string identifier, string password)
        {
            Identity identity = await _authenticationDbContext.Identities
                .SingleOrDefaultAsync(i => i.AuthenticationIdentities.OfType<PasswordIdentity>().Any(p => p.Identifier == identifier));

            if (!(identity?.AuthenticationIdentities.SingleOrDefault(a => a is PasswordIdentity) is PasswordIdentity passwordIdentity))
            {
                return null;
            }

            return await _checkPasswordCommand.ExecuteAsync(passwordIdentity,
                new CheckPasswordDdto
                {
                    Password = password
                })
                ? identity
                : null;
        }
    }
}