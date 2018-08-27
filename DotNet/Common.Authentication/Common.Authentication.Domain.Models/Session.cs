using System;
using System.ComponentModel.DataAnnotations;
using Common.Domain.Models;

namespace Common.Authentication.Domain.Models
{
    public class Session : AggregateRoot
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
        public bool IsRevoked { get; set; }
    }
}