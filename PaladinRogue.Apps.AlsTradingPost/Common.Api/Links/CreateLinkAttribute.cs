using System;

namespace Common.Api.Links
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CreateLinkAttribute : LinkAttribute
    {
        public CreateLinkAttribute(string uriName) 
            : base(LinkType.Create, uriName, Constants.HttpVerbs.Post)
        {
        }
    }
}