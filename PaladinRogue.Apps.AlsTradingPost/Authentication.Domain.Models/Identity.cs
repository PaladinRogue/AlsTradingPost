using System.ComponentModel.DataAnnotations;
using Common.Domain.Models;
using Common.Domain.Models.DataProtection;

namespace Authentication.Domain.Models
{
    public class Identity : AggregateRoot
    {
        [MaxLength(100)]
        [SensitiveInformation]
        public string AuthenticationId { get; set; }
    }
}
