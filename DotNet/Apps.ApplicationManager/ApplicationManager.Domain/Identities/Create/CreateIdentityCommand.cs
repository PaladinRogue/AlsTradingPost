using System.Collections.Generic;
using ApplicationManager.Domain.Identities.Queries;
using Common.Domain.Exceptions;
using Common.Domain.Validation;
using FluentValidation;

namespace ApplicationManager.Domain.Identities.Create
{
    public class CreateIdentityCommand : ICreateIdentityCommand
    {
        private readonly IValidator<CreateIdentityCommandDdto> _validator;

        private readonly IGetIdentityByEmailAddressQuery _getIdentityByEmailAddressQuery;

        public CreateIdentityCommand(
            IValidator<CreateIdentityCommandDdto> validator,
            IGetIdentityByEmailAddressQuery getIdentityByEmailAddressQuery)
        {
            _validator = validator;
            _getIdentityByEmailAddressQuery = getIdentityByEmailAddressQuery;
        }

        public Identity Execute(CreateIdentityCommandDdto createIdentityCommandDdto)
        {
            _validator.ValidateAndThrow(createIdentityCommandDdto);

            if (_getIdentityByEmailAddressQuery.Run(createIdentityCommandDdto.EmailAddress) != null)
            {
                throw new DomainValidationRuleException(new ValidationResult
                {
                    PropertyValidationErrors = new List<PropertyValidationError>
                    {
                        PropertyValidationErrorFactory.Create(nameof(createIdentityCommandDdto.EmailAddress), createIdentityCommandDdto.EmailAddress, ValidationErrorCodes.NotUnique)
                    }
                });
            }

            return Identity.Create(new CreateIdentityDdto
            {
                EmailAddress = createIdentityCommandDdto.EmailAddress
            });
        }
    }
}
