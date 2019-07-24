using System.Linq;
using System.Threading.Tasks;
using Authentication.Domain.Identities;
using Authentication.Domain.Identities.CheckPassword;
using Authentication.Domain.Identities.Queries;
using Microsoft.EntityFrameworkCore;

namespace Authtentication.Persistence.Identities
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