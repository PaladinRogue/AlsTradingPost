﻿using System;
using Common.Domain.Concurrency;
using Common.Domain.Models;
using Common.Resources.Authentication;

namespace Authentication.Domain.ApplicationServices.Models
{
    public class ApplicationProjection : VersionedProjection
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public AuthenticationProtocol AuthenticationProtocols { get; set; }
	}
}
