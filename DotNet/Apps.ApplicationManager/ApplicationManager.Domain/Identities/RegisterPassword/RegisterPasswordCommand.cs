using System;
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

        public RegisterPasswordCommand(
            IValidator<RegisterPasswordCommandDdto> validator,
            IPasswordIdentityIdentifierIsUniqueQuery passwordIdentityIdentifierIsUniqueQuery)
        {
            _validator = validator;
            _passwordIdentityIdentifierIsUniqueQuery = passwordIdentityIdentifierIsUniqueQuery;
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

            return identity.RegisterPassword(authenticationGrantTypePassword, new RegisterPasswordDdto
            {
                Identifier = registerPasswordCommandDdto.Identifier,
                Password = registerPasswordCommandDdto.Password,
                EmailAddress = registerPasswordCommandDdto.EmailAddress
            });
        }
    }
}