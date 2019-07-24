using Authentication.Domain.Identities;

namespace Authentication.Domain.Users.Create
{
    public class CreateUserCommandDdto
    {
        public Identity Identity { get; set; }
    }
}