using System;

namespace PaladinRogue.Libray.Authorisation.Common.Contexts
{
    public class DefaultAuthorisationContext : IAuthorisationContext
    {
        private DefaultAuthorisationContext(string authorisationResource, string authorisationAction)
        {
            Action = authorisationAction;
            Resource = authorisationResource;
        }

        public string Resource { get; }

        public string Action { get; }

        public Type ResourceType => null;

        public Guid? ResourceId => null;

        public static DefaultAuthorisationContext Create(string authorisationResource, string authorisationAction)
        {
            if (string.IsNullOrWhiteSpace(authorisationResource))
            {
                throw new ArgumentNullException(nameof(authorisationResource));
            }

            if (string.IsNullOrWhiteSpace(authorisationAction))
            {
                throw new ArgumentNullException(nameof(authorisationAction));
            }

            return new DefaultAuthorisationContext(authorisationResource, authorisationAction);
        }
    }
}
