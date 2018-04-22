using System;

namespace Common.Api.Links
{
    [AttributeUsage(AttributeTargets.Class)]
    public class LinkAttribute : Attribute
    {
        public string LinkName { get; }

        public string UriName { get; }

        public string[] HttpVerbs { get; }

        public LinkAttribute(string linkName, string uriName, params string[] httpVerbs)
        {
            LinkName = linkName;
            UriName = uriName;
            HttpVerbs = httpVerbs;
        }
    }
}