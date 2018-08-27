using System;
using Common.Domain.Concurrency;
using Common.Resources.Authentication;

namespace Authentication.Domain.ApplicationServices.Models
{
    public class UpdateApplicationDdto : VersionedDdto
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public AuthenticationProtocol AuthenticationProtocols { get; set; }

	    public string AuthenticationEndpoint { get; set; }
    }
}
