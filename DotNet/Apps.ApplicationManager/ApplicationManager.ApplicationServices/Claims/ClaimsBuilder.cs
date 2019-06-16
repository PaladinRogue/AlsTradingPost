using System;
using System.Security.Claims;

namespace ApplicationManager.ApplicationServices.Claims
{
    public class ClaimsBuilder : Common.Application.Claims.ClaimsBuilder
    {
        protected ClaimsBuilder()
        {
        }

        public new static ClaimsBuilder CreateBuilder()
        {
            return new ClaimsBuilder();
        }

        public ClaimsBuilder WithUser(Guid userId)
        {
            Claims.AddClaim(new Claim(JwtClaimIdentifiers.User, userId.ToString()));

            return this;
        }
    }
}