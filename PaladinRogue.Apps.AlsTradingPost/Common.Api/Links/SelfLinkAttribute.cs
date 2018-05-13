using System;

namespace Common.Api.Links
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class SelfLinkAttribute : LinkAttribute
    {
        public SelfLinkAttribute(string uriName, params string[] httpVerbs) 
            : base(LinkType.Self, uriName, httpVerbs)
        {
        }
    }
}