using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationManager.ApplicationServices.Authentication.Models;
using ApplicationManager.Domain.Identities;
using ApplicationManager.Domain.Identities.Login;
using ApplicationManager.Domain.Users;
using Common.ApplicationServices.Authentication;
using Common.ApplicationServices.Exceptions;
using Common.ApplicationServices.Transactions;
using Common.Domain.Exceptions;
using Common.Domain.Persistence;
using ClaimsBuilder = ApplicationManager.ApplicationServices.Claims.ClaimsBuilder;
using JwtClaims = Common.Api.Authentication.Constants.JwtClaims;

namespace ApplicationManager.ApplicationServices.Authentication
{
    public class AuthenticationApplicationService : IAuthenticationApplicationService
    {
        private readonly ICommandRepository<User> _userCommandRepository;

        private readonly ILoginCommand _loginCommand;

        private readonly IJwtFactory _jwtFactory;

        private readonly ITransactionManager _transactionManager;

        public AuthenticationApplicationService(
            ICommandRepository<User> userCommandRepository,
            ILoginCommand loginCommand,
            IJwtFactory jwtFactory,
            ITransactionManager transactionManager)
        {
            _userCommandRepository = userCommandRepository;
            _loginCommand = loginCommand;
            _jwtFactory = jwtFactory;
            _transactionManager = transactionManager;
        }

        public async Task<JwtAdto> Password(PasswordAdto passwordAdto)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    Identity identity = _loginCommand.Execute(new LoginCommandDdto
                    {
                        Identifier = passwordAdto.Identifier,
                        Password = passwordAdto.Password
                    });

                    User user = _userCommandRepository.GetSingle(u => u.Identity.Id == identity.Id);

                    ClaimsBuilder claimsBuilder = ClaimsBuilder.CreateBuilder();

                    if (user != null)
                    {
                        claimsBuilder.WithUser(user.Id);
                    }

                    claimsBuilder.WithSubject(identity.Id);

                    if (identity.AuthenticationIdentities.Any(i => i is TwoFactorAuthenticationIdentity authenticationIdentity
                                                                   && authenticationIdentity.TwoFactorAuthenticationType == TwoFactorAuthenticationType.ConfirmIdentity))
                    {
                        claimsBuilder.WithRole(JwtClaims.RestrictedAppAccess);
                    }
                    else
                    {
                        claimsBuilder.WithRole(JwtClaims.AppAccess);
                    }

                    ClaimsIdentity claimsIdentity = claimsBuilder.Build();

                    transaction.Commit();

                    return await _jwtFactory.GenerateJwt<JwtAdto>(claimsIdentity, identity.Session.Id);
                }
                catch (DomainValidationRuleException e)
                {
                    throw new BusinessValidationRuleApplicationException(e.ValidationResult);
                }

            }
        }
    }
}