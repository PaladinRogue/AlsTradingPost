using System.Threading.Tasks;
using FluentValidation;

namespace PaladinRogue.Libray.Vault.Domain.Applications.Create
{
    public class CreateApplicationCommand : ICreateApplicationCommand
    {
        private readonly IValidator<CreateApplicationCommandDdto> _validator;

        public CreateApplicationCommand(IValidator<CreateApplicationCommandDdto> validator)
        {
            _validator = validator;
        }

        public Task<Application> ExecuteAsync(CreateApplicationCommandDdto createApplicationCommandDdto)
        {
            _validator.ValidateAndThrow(createApplicationCommandDdto);

            Application application = Application.Create(new CreateApplicationDdto
            {
                SystemName = createApplicationCommandDdto.SystemName
            });

            return Task.FromResult(application);
        }
    }
}