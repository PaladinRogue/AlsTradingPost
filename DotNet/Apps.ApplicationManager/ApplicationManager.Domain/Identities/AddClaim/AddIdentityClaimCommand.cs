using System.Threading.Tasks;
using Common.Domain.Validation;
using FluentValidation;

namespace ApplicationManager.Domain.Identities.AddClaim
{
    public class AddIdentityClaimCommand : IAddIdentityClaimCommand
    {
        private readonly IValidator<AddIdentityClaimCommandDdto> _validator;

        public AddIdentityClaimCommand(IValidator<AddIdentityClaimCommandDdto> validator)
        {
            _validator = validator;
        }

        public Task ExecuteAsync(
            Identity identity,
            AddIdentityClaimCommandDdto addIdentityClaimCommandDdto)
        {
            _validator.ValidateAndThrow(addIdentityClaimCommandDdto);

            identity.AddClaim(new AddIdentityClaimDdto
            {
                Type = addIdentityClaimCommandDdto.Type,
                Value = addIdentityClaimCommandDdto.Value
            });

            return Task.CompletedTask;
        }
    }
}