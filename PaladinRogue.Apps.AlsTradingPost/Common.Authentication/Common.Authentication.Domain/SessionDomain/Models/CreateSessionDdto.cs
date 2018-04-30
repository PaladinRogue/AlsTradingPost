namespace Common.Authentication.Domain.SessionDomain.Models
{
    public class CreateSessionDdto
    {
        public string RefreshToken { get; set; }
        public bool Revoked { get; set; }
    }
}