using System;

namespace Common.Application.Authorisation
{
    public class AuthorisationRule : IAuthorisationRule
    {
        private AuthorisationRule(string authorisationResource, string authorisationAction)
        {
            Action = authorisationAction;
            Resource = authorisationResource;
        }

        public string Resource { get; }
        public string Action { get; }

        public static AuthorisationRule Create(string authorisationResource, string authorisationAction)
        {
            if (string.IsNullOrWhiteSpace(authorisationResource))
            {
                throw new ArgumentNullException(nameof(authorisationResource));
            }

            if (string.IsNullOrWhiteSpace(authorisationAction))
            {
                throw new ArgumentNullException(nameof(authorisationAction));
            }

            return new AuthorisationRule(authorisationResource, authorisationAction);
        }
    }
}
