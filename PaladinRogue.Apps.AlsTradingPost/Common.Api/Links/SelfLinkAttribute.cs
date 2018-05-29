using System;

namespace Common.Api.Links
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class SelfLinkAttribute : LinkAttribute
    {
        public SelfLinkAttribute(string uriName, string httpVerb) 
            : base(LinkType.Self, uriName, httpVerb)
        {
        }

        public SelfLinkAttribute(string uriName, string httpVerb, Type authorisationContextType) 
            : base(LinkType.Self, uriName, httpVerb, authorisationContextType)
        {
        }
    }
}