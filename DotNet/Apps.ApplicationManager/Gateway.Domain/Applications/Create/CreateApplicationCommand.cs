using System.Threading.Tasks;
using FluentValidation;

namespace PaladinRogue.Gateway.Domain.Applications.Create
{
    public class CreateApplicationCommand : ICreateApplicationCommand
    {
        private readonly IValidator<CreateApplicationDdto> _validator;

        public CreateApplicationCommand(IValidator<CreateApplicationDdto> validator)
        {
            _validator = validator;
        }

        public Task<Application> ExecuteAsync(CreateApplicationDdto createApplicationDdto)
        {
            _validator.ValidateAndThrow(createApplicationDdto);

            return Task.FromResult(Application.Create(createApplicationDdto));
        }
    }
}
