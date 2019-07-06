using System.Linq;
using System.Threading.Tasks;
using ApplicationManager.Domain.Identities;
using ApplicationManager.Domain.Identities.CheckPassword;
using ApplicationManager.Domain.Identities.Queries;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Identity> RunAsync(string identifier, string password)
        {
            Identity identity = await _applicationManagerDbContext.Identities
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