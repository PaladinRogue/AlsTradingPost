using System.Threading.Tasks;
using FluentValidation;
using PaladinRogue.Authentication.Domain.Identities.Queries;
using PaladinRogue.Library.Core.Domain.Validation;

namespace PaladinRogue.Authentication.Domain.Identities.ForgotPassword
{
    public class ForgotPasswordCommand : IForgotPasswordCommand
    {
        private readonly IValidator<ForgotPasswordCommandDdto> _validator;

        private readonly IGetIdentityByEmailAddressQuery _getIdentityByEmailAddressQuery;

        public ForgotPasswordCommand(
            IGetIdentityByEmailAddressQuery getIdentityByEmailAddressQuery,
            IValidator<ForgotPasswordCommandDdto> validator)
        {
            _getIdentityByEmailAddressQuery = getIdentityByEmailAddressQuery;
            _validator = validator;
        }

        public async Task<Identity> ExecuteAsync(ForgotPasswordCommandDdto forgotPasswordCommandDdto)
        {
            _validator.ValidateAndThrow(forgotPasswordCommandDdto);

            Identity identity = await _getIdentityByEmailAddressQuery.RunAsync(forgotPasswordCommandDdto.EmailAddress);

            identity?.ForgotPassword(new ForgotPasswordDdto
            {
                EmailAddress = forgotPasswordCommandDdto.EmailAddress
            });

            return identity;
        }
    }
}