using System;
using Common.Setup.Infrastructure.Constants;

namespace Common.Api.Links
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class SelfLinkAttribute : LinkAttribute
    {
        public SelfLinkAttribute(string uriName, HttpVerb httpVerb)
            : base(LinkType.Self, uriName, httpVerb)
        {
        }

        public SelfLinkAttribute(string uriName, HttpVerb httpVerb, Type authorisationContextType)
            : base(LinkType.Self, uriName, httpVerb, authorisationContextType)
        {
        }
    }
}