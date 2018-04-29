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

        public ClaimsBuilder WithPersonas(Persona personas)
        {
            Claims.AddClaim(new Claim(JwtClaimIdentifiers.Persona, personas.ToString()));

            return this;
        }
    }
}
