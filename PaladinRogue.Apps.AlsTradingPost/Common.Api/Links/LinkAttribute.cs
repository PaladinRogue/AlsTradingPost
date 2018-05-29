using System;
using Common.Application.Authorisation;

namespace Common.Api.Links
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class LinkAttribute : Attribute
    {
        public string LinkName { get; }

        public string UriName { get; }

        public string HttpVerb { get; }

        public Type AuthorisationContextType { get; }
        
        public LinkAttribute(string linkName, string uriName, string httpVerb)
        {
            LinkName = linkName;
            UriName = uriName;
            HttpVerb = httpVerb;
        }

        public LinkAttribute(string linkName, string uriName, string httpVerb, Type authorisationContextType)
        {
            LinkName = linkName;
            UriName = uriName;
            HttpVerb = httpVerb;

            if (authorisationContextType.IsInstanceOfType(typeof(IAuthorisationContext)))
            {
                throw new ArgumentOutOfRangeException(nameof(authorisationContextType));
            }

            AuthorisationContextType = authorisationContextType;
        }
    }
}