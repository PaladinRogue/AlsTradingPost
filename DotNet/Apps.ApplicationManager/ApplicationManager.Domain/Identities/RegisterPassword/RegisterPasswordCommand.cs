using System.Collections.Generic;
using ApplicationManager.Domain.AuthenticationServices;
using ApplicationManager.Domain.Identities.Queries;
using Common.Domain.Exceptions;
using Common.Domain.Validation;
using FluentValidation;

namespace ApplicationManager.Domain.Identities.RegisterPassword
{
    public class RegisterPasswordCommand : IRegisterPasswordCommand
    {
        private readonly IValidator<RegisterPasswordDdto> _validator;

        private readonly IPasswordIdentityIdentifierIsUniqueQuery _passwordIdentityIdentifierIsUniqueQuery;

        public RegisterPasswordCommand(
            IValidator<RegisterPasswordDdto> validator,
            IPasswordIdentityIdentifierIsUniqueQuery passwordIdentityIdentifierIsUniqueQuery)
        {
            _validator = validator;
            _passwordIdentityIdentifierIsUniqueQuery = passwordIdentityIdentifierIsUniqueQuery;
        }

        public PasswordIdentity Execute(Identity identity, AuthenticationGrantTypePassword authenticationGrantTypePassword, RegisterPasswordDdto registerPasswordDdto)
        {
            _validator.ValidateAndThrow(registerPasswordDdto);

            if (!_passwordIdentityIdentifierIsUniqueQuery.Run(registerPasswordDdto.Identifier))
            {
                throw new DomainValidationRuleException(new ValidationResult
                {
                    PropertyValidationErrors = new List<PropertyValidationError>
                    {
                        PropertyValidationErrorFactory.Create(nameof(registerPasswordDdto.Identifier), registerPasswordDdto.Identifier, ValidationErrorCodes.NotUnique)
                    }
                });
            }

            return identity.RegisterPassword(authenticationGrantTypePassword, registerPasswordDdto);
        }
    }
}