using System;
using System.Collections.Generic;
using Authentication.Domain.Models;
using Authentication.Domain.Models.Enum;
using Common.Domain.Models;

namespace Authentication.Domain.ApplicationServices.Models
{
    public class ApplicationProjection : VersionedProjection
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public AuthenticationProtocol AuthenticationProtocols { get; set; }
	}
}
