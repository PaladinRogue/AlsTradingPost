using System;

namespace Authentication.Domain.IdentityServices.Models
{
    public class LoginIdentityProjection
    {
        public Guid Id { get; set; }
        
        public Guid SessionId { get; set; }
        
        public string RefreshToken { get; set; }
    }
}
