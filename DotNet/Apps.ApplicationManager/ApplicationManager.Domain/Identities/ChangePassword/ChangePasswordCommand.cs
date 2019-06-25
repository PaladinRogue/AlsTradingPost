using Common.Domain.Validation;
using FluentValidation;

namespace ApplicationManager.Domain.Identities.ChangePassword
{
    public class ChangePasswordCommand : IChangePasswordCommand
    {
        private readonly IValidator<ChangePasswordCommandDdto> _validator;

        public ChangePasswordCommand(IValidator<ChangePasswordCommandDdto> validator)
        {
            _validator = validator;
        }

        public void Execute(Identity identity, ChangePasswordCommandDdto changePasswordCommandDdto)
        {
            _validator.ValidateAndThrow(changePasswordCommandDdto);

            identity.ChangePassword(new ChangePasswordDdto
            {
                Password = changePasswordCommandDdto.Password
            });
        }
    }
}