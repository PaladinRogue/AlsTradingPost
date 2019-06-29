namespace ApplicationManager.Domain.Users.Create
{
    public interface ICreateUserCommand
    {
        User Execute(CreateUserCommandDdto createUserCommandDdto);
    }
}