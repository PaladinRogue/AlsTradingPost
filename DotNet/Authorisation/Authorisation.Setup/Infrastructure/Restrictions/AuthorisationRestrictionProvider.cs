using System;
using System.Collections.Generic;
using System.Linq;

namespace Authorisation.Application.Restrictions
{
    public class AuthorisationRestrictionProvider : IAuthorisationRestrictionProvider
    {
        private readonly IEnumerable<IAuthorisationRestriction> _authorisationRestrictions;

        public AuthorisationRestrictionProvider(
            IEnumerable<IAuthorisationRestriction> authorisationRestrictions)
        {
            _authorisationRestrictions = authorisationRestrictions;
        }

        public IAuthorisationRestriction GetByRestriction(string restriction)
        {
            IAuthorisationRestriction authorisationRestriction = _authorisationRestrictions.SingleOrDefault(a => a.Restriction.Equals(restriction, StringComparison.OrdinalIgnoreCase));

            if (authorisationRestriction == null)
            {
                throw new AuthorisationRestrictionNotFoundException($"Authorisation restriction not defined for restriction: { restriction }");
            }

            return authorisationRestriction;
        }
    }
}