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
        private readonly IValidator<RegisterPasswordCommandDdto> _validator;

        private readonly IPasswordIdentityIdentifierIsUniqueQuery _passwordIdentityIdentifierIsUniqueQuery;

        private readonly IPasswordIdentityEmailIsUniqueQuery _passwordIdentityEmailIsUniqueQuery;

        public RegisterPasswordCommand(
            IValidator<RegisterPasswordCommandDdto> validator,
            IPasswordIdentityIdentifierIsUniqueQuery passwordIdentityIdentifierIsUniqueQuery,
            IPasswordIdentityEmailIsUniqueQuery passwordIdentityEmailIsUniqueQuery)
        {
            _validator = validator;
            _passwordIdentityIdentifierIsUniqueQuery = passwordIdentityIdentifierIsUniqueQuery;
            _passwordIdentityEmailIsUniqueQuery = passwordIdentityEmailIsUniqueQuery;
        }

        public PasswordIdentity Execute(Identity identity, AuthenticationGrantTypePassword authenticationGrantTypePassword, RegisterPasswordCommandDdto registerPasswordCommandDdto)
        {
            _validator.ValidateAndThrow(registerPasswordCommandDdto);

            if (!_passwordIdentityIdentifierIsUniqueQuery.Run(registerPasswordCommandDdto.Identifier))
            {
                throw new DomainValidationRuleException(new ValidationResult
                {
                    PropertyValidationErrors = new List<PropertyValidationError>
                    {
                        PropertyValidationErrorFactory.Create(nameof(registerPasswordCommandDdto.Identifier), registerPasswordCommandDdto.Identifier, ValidationErrorCodes.NotUnique)
                    }
                });
            }

            if (_passwordIdentityEmailIsUniqueQuery.Run(registerPasswordCommandDdto.EmailAddress))
            {
                throw new DomainValidationRuleException(new ValidationResult
                {
                    PropertyValidationErrors = new List<PropertyValidationError>
                    {
                        PropertyValidationErrorFactory.Create(nameof(registerPasswordCommandDdto.EmailAddress), registerPasswordCommandDdto.EmailAddress, ValidationErrorCodes.NotUnique)
                    }
                });
            }

            return identity.RegisterPassword(authenticationGrantTypePassword, new RegisterPasswordDdto
            {
                Identifier = registerPasswordCommandDdto.Identifier,
                Password = registerPasswordCommandDdto.Password,
                EmailAddress = registerPasswordCommandDdto.EmailAddress
            });
        }
    }
}