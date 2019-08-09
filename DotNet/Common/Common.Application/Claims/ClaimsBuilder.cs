using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Common.Application.Claims.Constants;

namespace Common.Application.Claims
{
    public class ClaimsBuilder
    {
        protected readonly ClaimsIdentity Claims;

        protected ClaimsBuilder()
        {
            Claims = new ClaimsIdentity();
        }

        public static ClaimsBuilder CreateBuilder()
        {
            return new ClaimsBuilder();
        }

        public ClaimsBuilder WithSubject(Guid id)
        {
            Claims.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, id.ToString()));

            return this;
        }

        public ClaimsBuilder WithRole(string role)
        {
            Claims.AddClaim(new Claim(JwtClaimIdentifiers.Rol, role));

            return this;
        }

        public ClaimsBuilder AddClaim(
            string type,
            string value)
        {
            Claims.AddClaim(new Claim(type, value));

            return this;
        }

        public ClaimsIdentity Build()
        {
            return Claims;
        }
    }
}
