using System.Threading.Tasks;
using Common.Domain.Validation;
using FluentValidation;

namespace KeyVault.Domain.Applications.AddDataKey
{
    public class AddApplicationDataKeyCommand : IAddApplicationDataKeyCommand
    {
        private readonly IValidator<AddApplicationDataKeyCommandDdto> _validator;

        public AddApplicationDataKeyCommand(IValidator<AddApplicationDataKeyCommandDdto> validator)
        {
            _validator = validator;
        }

        public Task<ApplicationDataKey> ExecuteAsync(Application application, AddApplicationDataKeyCommandDdto addApplicationDataKeyCommandDdto)
        {
            _validator.ValidateAndThrow(addApplicationDataKeyCommandDdto);

            ApplicationDataKey applicationDataKey = application.AddDataKey(new AddApplicationDataKeyDdto
            {
                Type = addApplicationDataKeyCommandDdto.Type,
                Value = addApplicationDataKeyCommandDdto.Value
            });

            return Task.FromResult(applicationDataKey);
        }
    }
}