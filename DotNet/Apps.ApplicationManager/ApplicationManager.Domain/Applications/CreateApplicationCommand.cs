using Common.Domain.Validation;
using FluentValidation;

namespace ApplicationManager.Domain.Applications
{
    public class CreateApplicationCommand : ICreateApplicationCommand
    {
        private readonly IValidator<CreateApplicationDdto> _validator;

        public CreateApplicationCommand(IValidator<CreateApplicationDdto> validator)
        {
            _validator = validator;
        }

        public Application Execute(CreateApplicationDdto createApplicationDdto)
        {
            _validator.ValidateAndThrow(createApplicationDdto);

            return Application.Create(createApplicationDdto);
        }
    }
}
