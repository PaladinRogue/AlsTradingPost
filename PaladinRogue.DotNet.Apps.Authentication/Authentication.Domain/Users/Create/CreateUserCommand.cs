using System.Threading.Tasks;
using FluentValidation;

namespace PaladinRogue.Authentication.Domain.Users.Create
{
    public class CreateUserCommand : ICreateUserCommand
    {
        private readonly IValidator<CreateUserCommandDdto> _validator;

        public CreateUserCommand(IValidator<CreateUserCommandDdto> validator)
        {
            _validator = validator;
        }

        public Task<User> ExecuteAsync(CreateUserCommandDdto createUserCommandDdto)
        {
            _validator.ValidateAndThrow(createUserCommandDdto);

            return User.Create(createUserCommandDdto.Identity);
        }
    }
}