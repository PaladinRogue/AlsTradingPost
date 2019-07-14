using System.Threading.Tasks;
using Common.Domain.Validation;
using FluentValidation;

namespace ApplicationManager.Domain.Identities.ChangeClaim
{
    public class ChangeIdentityClaimCommand : IChangeIdentityClaimCommand
    {
        private readonly IValidator<ChangeIdentityClaimCommandDdto> _validator;

        public ChangeIdentityClaimCommand(IValidator<ChangeIdentityClaimCommandDdto> validator)
        {
            _validator = validator;
        }

        public Task ExecuteAsync(
            Identity identity,
            ChangeIdentityClaimCommandDdto changeIdentityClaimCommandDdto)
        {
            _validator.ValidateAndThrow(changeIdentityClaimCommandDdto);

            identity.ChangeClaim(new ChangeIdentityClaimDdto
            {
                Type = changeIdentityClaimCommandDdto.Type,
                Value = changeIdentityClaimCommandDdto.Value
            });

            return Task.CompletedTask;
        }
    }
}