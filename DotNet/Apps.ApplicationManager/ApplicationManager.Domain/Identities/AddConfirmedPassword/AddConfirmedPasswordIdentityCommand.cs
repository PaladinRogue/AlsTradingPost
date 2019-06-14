using System.Collections.Generic;
using ApplicationManager.Domain.AuthenticationServices;
using ApplicationManager.Domain.Identities.Queries;
using Common.Domain.Exceptions;
using Common.Domain.Validation;
using FluentValidation;

namespace ApplicationManager.Domain.Identities.AddConfirmedPassword
{
    public class AddConfirmedPasswordIdentityCommand : IAddConfirmedPasswordIdentityCommand
    {
        private readonly IValidator<AddConfirmedPasswordIdentityDdto> _validator;

        private readonly IPasswordIdentityIdentifierIsUniqueQuery _passwordIdentityIdentifierIsUniqueQuery;

        public AddConfirmedPasswordIdentityCommand(
            IValidator<AddConfirmedPasswordIdentityDdto> validator,
            IPasswordIdentityIdentifierIsUniqueQuery passwordIdentityIdentifierIsUniqueQuery)
        {
            _validator = validator;
            _passwordIdentityIdentifierIsUniqueQuery = passwordIdentityIdentifierIsUniqueQuery;
        }

        public PasswordIdentity Execute(
            Identity identity,
            AuthenticationGrantTypePassword authenticationGrantTypePassword,
            AddConfirmedPasswordIdentityDdto addConfirmedPasswordIdentityDdto)
        {
            _validator.ValidateAndThrow(addConfirmedPasswordIdentityDdto);

            if (!_passwordIdentityIdentifierIsUniqueQuery.Run(addConfirmedPasswordIdentityDdto.Identifier))
            {
                throw new DomainValidationRuleException(new ValidationResult
                {
                    PropertyValidationErrors = new List<PropertyValidationError>
                    {
                        PropertyValidationErrorFactory.Create(nameof(addConfirmedPasswordIdentityDdto.Identifier), addConfirmedPasswordIdentityDdto.Identifier, ValidationErrorCodes.NotUnique)
                    }
                });
            }

            return identity.AddConfirmedPasswordIdentity(authenticationGrantTypePassword, addConfirmedPasswordIdentityDdto);
        }
    }
}