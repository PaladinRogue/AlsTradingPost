using System;

namespace Common.Api.Links
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SearchLinkAttribute : LinkAttribute
    {
        public SearchLinkAttribute(string uriName) 
            : base(LinkType.Search, uriName, Setup.Infrastructure.Constants.HttpVerbs.Get)
        {
        }
    }
}