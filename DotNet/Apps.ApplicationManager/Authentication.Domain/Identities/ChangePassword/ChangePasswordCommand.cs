using System.Threading.Tasks;
using Common.Domain.Validation;
using FluentValidation;

namespace Authentication.Domain.Identities.ChangePassword
{
    public class ChangePasswordCommand : IChangePasswordCommand
    {
        private readonly IValidator<ChangePasswordCommandDdto> _validator;

        public ChangePasswordCommand(IValidator<ChangePasswordCommandDdto> validator)
        {
            _validator = validator;
        }

        public Task ExecuteAsync(Identity identity,
            ChangePasswordCommandDdto changePasswordCommandDdto)
        {
            _validator.ValidateAndThrow(changePasswordCommandDdto);

            return identity.ChangePassword(new ChangePasswordDdto
            {
                Password = changePasswordCommandDdto.Password
            });
        }
    }
}