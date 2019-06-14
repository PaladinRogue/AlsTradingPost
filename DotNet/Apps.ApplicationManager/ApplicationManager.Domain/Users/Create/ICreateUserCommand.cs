namespace ApplicationManager.Domain.Users.Create
{
    public interface ICreateUserCommand
    {
        User Execute(CreateUserDdto createUserDdto);
    }
}