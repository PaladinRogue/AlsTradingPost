using System.Threading.Tasks;
using Common.Domain.Validation;
using FluentValidation;

namespace ApplicationManager.Domain.Identities.AddOrChangeClaim
{
    public class AddOrChangeIdentityClaimCommand : IAddOrChangeIdentityClaimCommand
    {
        private readonly IValidator<AddOrChangeIdentityClaimCommandDdto> _validator;

        public AddOrChangeIdentityClaimCommand(IValidator<AddOrChangeIdentityClaimCommandDdto> validator)
        {
            _validator = validator;
        }

        public Task ExecuteAsync(
            Identity identity,
            AddOrChangeIdentityClaimCommandDdto addOrChangeIdentityClaimCommandDdto)
        {
            _validator.ValidateAndThrow(addOrChangeIdentityClaimCommandDdto);

            identity.AddOrChangeClaim(new AddOrChangeIdentityClaimDdto
            {
                Type = addOrChangeIdentityClaimCommandDdto.Type,
                Value = addOrChangeIdentityClaimCommandDdto.Value
            });

            return Task.CompletedTask;
        }
    }
}