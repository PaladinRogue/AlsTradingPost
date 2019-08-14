using System.Threading.Tasks;
using FluentValidation;
using PaladinRogue.Library.Core.Domain.Validation;

namespace PaladinRogue.Authentication.Domain.Identities.AddOrChangeClaim
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