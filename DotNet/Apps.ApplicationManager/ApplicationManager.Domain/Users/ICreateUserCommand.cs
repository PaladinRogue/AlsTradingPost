namespace ApplicationManager.Domain.Users
{
    public interface ICreateUserCommand
    {
        User Execute(CreateUserDdto createUserDdto);
    }
}