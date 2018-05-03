using System;
using Common.Domain.Models;

namespace Common.Authentication.Domain.Models
{
    public class Session : VersionedEntity
    {
        public Session()
        {
            
        }
        
        public Session(Guid userId)
        {
            Id = userId;
        }
        
        public string RefreshToken { get; set; }
        public bool Revoked { get; set; }
    }
}