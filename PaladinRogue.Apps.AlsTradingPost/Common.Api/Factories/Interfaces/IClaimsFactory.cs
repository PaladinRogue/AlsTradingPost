using System;
using System.Security.Claims;

namespace Common.Api.Factories.Interfaces
{
    public interface IClaimsFactory
    {
	    ClaimsIdentity GenerateClaimsIdentity(Guid id);
    }
}
