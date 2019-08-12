using System.Threading.Tasks;
using FluentValidation;

namespace PaladinRogue.Library.Vault.Domain.SharedDataKeys.Change
{
    public class ChangeSharedDataKeyCommand : IChangeSharedDataKeyCommand
    {
        private readonly IValidator<ChangeSharedDataKeyCommandDdto> _validator;

        public ChangeSharedDataKeyCommand(IValidator<ChangeSharedDataKeyCommandDdto> validator)
        {
            _validator = validator;
        }

        public Task ExecuteAsync(SharedDataKey sharedDataKey, ChangeSharedDataKeyCommandDdto changeSharedDataKeyCommandDdto)
        {
            _validator.ValidateAndThrow(changeSharedDataKeyCommandDdto);

            sharedDataKey.Change(new ChangeSharedDataKeyDdto
            {
                Value = changeSharedDataKeyCommandDdto.Value
            });

            return Task.CompletedTask;
        }
    }
}