using System.Threading.Tasks;
using FluentValidation;
using PaladinRogue.Library.Core.Domain.Validation;

namespace PaladinRogue.Authentication.Domain.Identities.ConfirmIdentity
{
    public class ConfirmIdentityCommand : IConfirmIdentityCommand
    {
        private readonly IValidator<ConfirmIdentityCommandDdto> _validator;

        public ConfirmIdentityCommand(IValidator<ConfirmIdentityCommandDdto> validator)
        {
            _validator = validator;
        }

        public Task ExecuteAsync(Identity identity,
            ConfirmIdentityCommandDdto confirmIdentityCommandDdto)
        {
            _validator.ValidateAndThrow(confirmIdentityCommandDdto);

            identity.ConfirmIdentity(new ConfirmIdentityDdto
            {
                Token = confirmIdentityCommandDdto.Token
            });

            return Task.CompletedTask;
        }
    }
}