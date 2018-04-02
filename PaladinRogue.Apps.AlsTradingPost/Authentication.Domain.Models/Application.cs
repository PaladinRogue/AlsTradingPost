using Authentication.Domain.Models.Enum;
using Common.Domain.Models;

namespace Authentication.Domain.Models
{
    public class Application : Entity
    {
	    public string Name { get; set; }
	    public AuthenticationProtocol AuthenticationProtocols { get; set; }
    }
}
