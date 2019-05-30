using Common.Domain.Validation;
using FluentValidation;

namespace ApplicationManager.Domain.Applications
{
    public class ChangeApplicationCommand : IChangeApplicationCommand
    {
        private readonly IValidator<ChangeApplicationDdto> _validator;

        public ChangeApplicationCommand(IValidator<ChangeApplicationDdto> validator)
        {
            _validator = validator;
        }

        public void Execute(Application application, ChangeApplicationDdto changeApplicationDdto)
        {
            _validator.ValidateAndThrow(changeApplicationDdto);

            application.Change(changeApplicationDdto);
        }
    }
}
