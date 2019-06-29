using ApplicationManager.Domain.Identities;

namespace ApplicationManager.Domain.Users.Create
{
    public class CreateUserCommandDdto
    {
        public Identity Identity { get; set; }
    }
}