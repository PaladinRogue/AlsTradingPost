using System;
using System.ComponentModel.DataAnnotations;
using Common.Domain.Models;

namespace Common.Authentication.Domain.Models
{
    public class Session : VersionedEntity
    {
        public Session()
        {
        }
        
        public Session(Guid id)
        {
            Id = id;
        }
        
        [MaxLength(100)]
        public string RefreshToken { get; set; }
        public bool Revoked { get; set; }
    }
}