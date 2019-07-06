using System.Threading.Tasks;
using FluentValidation;

namespace ApplicationManager.Domain.Users.Create
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

            return Task.FromResult(User.Create(createUserCommandDdto.Identity));
        }
    }
}