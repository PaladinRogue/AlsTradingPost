using Common.Resources.Authentication;

namespace Authentication.Application.Application.Models
{
    public class ApplicationAdto
    {
        public string Name { get; set; }

        public AuthenticationProtocol AuthenticationProtocols { get; set; }

        public string AuthenticationEndpoint { get; set; }
    }
}
