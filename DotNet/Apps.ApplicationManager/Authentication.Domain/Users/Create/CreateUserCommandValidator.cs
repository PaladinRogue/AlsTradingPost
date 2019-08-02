using FluentValidation;

namespace Authentication.Domain.Users.Create
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommandDdto>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(a => a)
                .NotNull();

            RuleFor(a => a.Identity)
                .NotNull();

        }
    }
}