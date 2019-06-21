using Common.Domain.Validation;
using FluentValidation;

namespace ApplicationManager.Domain.Identities.ChangePassword
{
    public class ChangePasswordCommand : IChangePasswordCommand
    {
        private readonly IValidator<ChangePasswordDdto> _validator;

        public ChangePasswordCommand(IValidator<ChangePasswordDdto> validator)
        {
            _validator = validator;
        }

        public void Execute(Identity identity, ChangePasswordDdto changePasswordDdto)
        {
            _validator.ValidateAndThrow(changePasswordDdto);

            identity.ChangePassword(changePasswordDdto);
        }
    }
}