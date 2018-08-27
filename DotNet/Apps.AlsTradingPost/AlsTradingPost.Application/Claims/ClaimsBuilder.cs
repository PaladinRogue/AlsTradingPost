using System;
using System.Security.Claims;
using AlsTradingPost.Resources;

namespace AlsTradingPost.Application.Claims
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

        public ClaimsBuilder WithPersonas(PersonaFlags personas)
        {
            Claims.AddClaim(new Claim(JwtClaimIdentifiers.Persona, personas.ToString()));

            return this;
        }

        public ClaimsBuilder WithTrader(Guid traderId)
        {
            Claims.AddClaim(new Claim(JwtClaimIdentifiers.Trader, traderId.ToString()));

            return this;
        }

        public ClaimsBuilder WithAdmin(Guid adminId)
        {
            Claims.AddClaim(new Claim(JwtClaimIdentifiers.Admin, adminId.ToString()));

            return this;
        }
    }
}
