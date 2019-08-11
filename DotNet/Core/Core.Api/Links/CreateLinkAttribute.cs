using System;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Libray.Core.Api.Links
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CreateLinkAttribute : LinkAttribute
    {
        public CreateLinkAttribute(string uriName)
            : base(LinkType.Create, uriName, HttpVerb.Post)
        {
        }

        public CreateLinkAttribute(
            string uriName,
            HttpVerb httpVerb,
            Type authorisationContextType = null)
            : base(LinkType.Create, uriName, httpVerb, authorisationContextType)
        {
        }
    }
}