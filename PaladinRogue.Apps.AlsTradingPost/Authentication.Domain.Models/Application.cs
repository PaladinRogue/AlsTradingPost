using Common.Domain.Models;
using Common.Resources.Authentication;

namespace Authentication.Domain.Models
{
    public class Application : Entity
    {
	    public string Name { get; set; }
	    public AuthenticationProtocol AuthenticationProtocols { get; set; }
    }
}
