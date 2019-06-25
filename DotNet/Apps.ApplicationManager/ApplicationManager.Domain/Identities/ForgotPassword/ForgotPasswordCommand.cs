using System;
using ApplicationManager.Domain.Identities.Queries;
using Common.Domain.Validation;
using FluentValidation;

namespace ApplicationManager.Domain.Identities.ForgotPassword
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

        public Identity Execute(ForgotPasswordCommandDdto forgotPasswordCommandDdto)
        {
            _validator.ValidateAndThrow(forgotPasswordCommandDdto);

            Identity identity = _getIdentityByEmailAddressQuery.Run(forgotPasswordCommandDdto.EmailAddress);

            identity?.ForgotPassword(new ForgotPasswordDdto
            {
                EmailAddress = forgotPasswordCommandDdto.EmailAddress
            });

            return identity;
        }
    }
}