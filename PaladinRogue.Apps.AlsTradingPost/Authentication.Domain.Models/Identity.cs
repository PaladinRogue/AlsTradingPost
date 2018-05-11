using System.ComponentModel.DataAnnotations;
using Common.Domain.Models;

namespace Authentication.Domain.Models
{
    public class Identity : AggregateRoot
    {
        [MaxLength(50)]
        public string AuthenticationId { get; set; }
    }
}
