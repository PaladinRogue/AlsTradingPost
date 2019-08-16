using System;
using PaladinRogue.Library.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Library.Core.Api.Links
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class AbsoluteLinkAttribute : LinkAttribute
    {
        public Type AbsoluteLinkProviderType { get; }

        public AbsoluteLinkAttribute(Type absoluteLinkProviderType, string linkName, HttpVerb httpVerb)
            : this(absoluteLinkProviderType, linkName, null, httpVerb, null)
        {
        }

        public AbsoluteLinkAttribute(Type absoluteLinkProviderType, string linkName, string uriName, HttpVerb httpVerb)
            : this(absoluteLinkProviderType, linkName, uriName, httpVerb, null)
        {
        }

        public AbsoluteLinkAttribute(Type absoluteLinkProviderType, string linkName, string uriName, HttpVerb httpVerb, Type authorisationContextType)
            : base(linkName, uriName, httpVerb, authorisationContextType)
        {
            if (absoluteLinkProviderType == null || !typeof(IAbsoluteLinkProvider).IsAssignableFrom(absoluteLinkProviderType))
            {
                throw new ArgumentOutOfRangeException(nameof(absoluteLinkProviderType));
            }

            AbsoluteLinkProviderType = absoluteLinkProviderType;
        }
    }
}