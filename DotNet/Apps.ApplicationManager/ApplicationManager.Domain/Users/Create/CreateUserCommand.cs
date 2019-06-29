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

        public User Execute(CreateUserCommandDdto createUserCommandDdto)
        {
            _validator.ValidateAndThrow(createUserCommandDdto);

            return User.Create(createUserCommandDdto.Identity);
        }
    }
}