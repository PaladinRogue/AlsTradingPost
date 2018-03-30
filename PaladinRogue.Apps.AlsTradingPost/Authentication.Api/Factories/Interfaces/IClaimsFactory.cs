using System;
using System.Security.Claims;

namespace Authentication.Api.Factories.Interfaces
{
    public interface IClaimsFactory
    {
	    ClaimsIdentity GenerateClaimsIdentity(Guid id, string accessToken);
    }
}
