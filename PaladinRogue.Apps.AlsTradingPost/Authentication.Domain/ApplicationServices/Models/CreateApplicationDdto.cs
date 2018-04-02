using Authentication.Domain.Models.Enum;

namespace Authentication.Domain.ApplicationServices.Models
{
    public class CreateApplicationDdto
    {
        public string Name { get; set; }
	    public AuthenticationProtocol AuthenticationProtocols { get; set; }
    }
}
