namespace ApplicationManager.Domain.Users
{
    public class CreateUserCommand : ICreateUserCommand
    {
        public User Execute(CreateUserDdto createUserDdto)
        {
            return User.Create(createUserDdto.Identity);
        }
    }
}