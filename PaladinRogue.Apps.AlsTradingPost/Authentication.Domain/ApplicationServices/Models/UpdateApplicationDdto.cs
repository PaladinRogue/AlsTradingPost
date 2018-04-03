using System;
using System.Collections.Generic;
using Authentication.Domain.Models;
using Common.Authentication.Enum;
using Common.Domain.Models;

namespace Authentication.Domain.ApplicationServices.Models
{
    public class UpdateApplicationDdto : VersionedDdto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public AuthenticationProtocol AuthenticationProtocols { get; set; }
	}
}
