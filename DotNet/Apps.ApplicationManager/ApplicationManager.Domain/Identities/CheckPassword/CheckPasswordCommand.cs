using Common.Domain.Validation;
using FluentValidation;

namespace ApplicationManager.Domain.Identities.CheckPassword
{
    public class CheckPasswordCommand : ICheckPasswordCommand
    {
        private readonly IValidator<CheckPasswordDdto> _validator;

        public CheckPasswordCommand(IValidator<CheckPasswordDdto> validator)
        {
            _validator = validator;
        }

        public bool Execute(PasswordIdentity passwordIdentity, CheckPasswordDdto checkPasswordDdto)
        {
            _validator.ValidateAndThrow(checkPasswordDdto);

            return passwordIdentity.CheckPassword(checkPasswordDdto);
        }
    }
}