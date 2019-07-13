using System.Collections.Generic;
using System.Threading.Tasks;
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

        private readonly IPasswordIdentityIdentifierExistsQuery _passwordIdentityIdentifierExistsQuery;

        private readonly IPasswordIdentityEmailExistsQuery _passwordIdentityEmailExistsQuery;

        public RegisterPasswordCommand(
            IValidator<RegisterPasswordCommandDdto> validator,
            IPasswordIdentityIdentifierExistsQuery passwordIdentityIdentifierExistsQuery,
            IPasswordIdentityEmailExistsQuery passwordIdentityEmailExistsQuery)
        {
            _validator = validator;
            _passwordIdentityIdentifierExistsQuery = passwordIdentityIdentifierExistsQuery;
            _passwordIdentityEmailExistsQuery = passwordIdentityEmailExistsQuery;
        }

        public async Task<PasswordIdentity> ExecuteAsync(Identity identity,
            AuthenticationGrantTypePassword authenticationGrantTypePassword,
            RegisterPasswordCommandDdto registerPasswordCommandDdto)
        {
            _validator.ValidateAndThrow(registerPasswordCommandDdto);

            if (await _passwordIdentityIdentifierExistsQuery.RunAsync(registerPasswordCommandDdto.Identifier))
            {
                throw new DomainValidationRuleException(new ValidationResult
                {
                    PropertyValidationErrors = new List<PropertyValidationError>
                    {
                        PropertyValidationErrorFactory.Create(nameof(registerPasswordCommandDdto.Identifier), registerPasswordCommandDdto.Identifier, ValidationErrorCodes.NotUnique)
                    }
                });
            }

            if (await _passwordIdentityEmailExistsQuery.RunAsync(registerPasswordCommandDdto.EmailAddress))
            {
                throw new DomainValidationRuleException(new ValidationResult
                {
                    PropertyValidationErrors = new List<PropertyValidationError>
                    {
                        PropertyValidationErrorFactory.Create(nameof(registerPasswordCommandDdto.EmailAddress), registerPasswordCommandDdto.EmailAddress, ValidationErrorCodes.NotUnique)
                    }
                });
            }

            return await identity.RegisterPassword(authenticationGrantTypePassword, new RegisterPasswordDdto
            {
                Identifier = registerPasswordCommandDdto.Identifier,
                Password = registerPasswordCommandDdto.Password,
                EmailAddress = registerPasswordCommandDdto.EmailAddress
            });
        }
    }
}