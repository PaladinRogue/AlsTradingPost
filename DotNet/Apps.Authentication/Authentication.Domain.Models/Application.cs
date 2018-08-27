using System.ComponentModel.DataAnnotations;
using Common.Domain.Models;
using Common.Resources.Authentication;

namespace Authentication.Domain.Models
{
    public class Application : AggregateRoot
    {
	    [MaxLength(20)]
	    public string Name { get; set; }
	    
	    public AuthenticationProtocol AuthenticationProtocols { get; set; }

        [MaxLength(100)]
        public string AuthenticationEndpoint { get; set; }
    }
}
