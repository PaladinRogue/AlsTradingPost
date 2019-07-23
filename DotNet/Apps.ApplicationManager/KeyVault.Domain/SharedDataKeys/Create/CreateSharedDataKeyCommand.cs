using System.Threading.Tasks;
using FluentValidation;

namespace KeyVault.Domain.SharedDataKeys.Create
{
    public class CreateSharedDataKeyCommand : ICreateSharedDataKeyCommand
    {
        private readonly IValidator<CreateSharedDataKeyCommandDdto> _validator;

        public CreateSharedDataKeyCommand(IValidator<CreateSharedDataKeyCommandDdto> validator)
        {
            _validator = validator;
        }

        public Task<SharedDataKey> ExecuteAsync(CreateSharedDataKeyCommandDdto createSharedDataKeyCommandDdto)
        {
            _validator.ValidateAndThrow(createSharedDataKeyCommandDdto);

            SharedDataKey sharedDataKey = SharedDataKey.Create(new CreateSharedDataKeyDdto
            {
                Name = createSharedDataKeyCommandDdto.Name,
                Value = createSharedDataKeyCommandDdto.Value
            });

            return Task.FromResult(sharedDataKey);
        }
    }
}