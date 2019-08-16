using System.Threading.Tasks;
using FluentValidation;
using PaladinRogue.Library.Core.Domain.Validation;

namespace PaladinRogue.Authentication.Domain.Identities.CheckPassword
{
    public class CheckPasswordCommand : ICheckPasswordCommand
    {
        private readonly IValidator<CheckPasswordDdto> _validator;

        public CheckPasswordCommand(IValidator<CheckPasswordDdto> validator)
        {
            _validator = validator;
        }

        public Task<bool> ExecuteAsync(PasswordIdentity passwordIdentity,
            CheckPasswordDdto checkPasswordDdto)
        {
            _validator.ValidateAndThrow(checkPasswordDdto);

            return passwordIdentity.CheckPassword(checkPasswordDdto);
        }
    }
}