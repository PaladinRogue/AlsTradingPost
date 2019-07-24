using System.Threading.Tasks;
using FluentValidation;

namespace Gateway.Domain.Applications.Change
{
    public class ChangeApplicationCommand : IChangeApplicationCommand
    {
        private readonly IValidator<ChangeApplicationDdto> _validator;

        public ChangeApplicationCommand(IValidator<ChangeApplicationDdto> validator)
        {
            _validator = validator;
        }

        public Task ExecuteAsync(Application application,
            ChangeApplicationDdto changeApplicationDdto)
        {
            _validator.ValidateAndThrow(changeApplicationDdto);

            application.Change(changeApplicationDdto);

            return Task.CompletedTask;
        }
    }
}
