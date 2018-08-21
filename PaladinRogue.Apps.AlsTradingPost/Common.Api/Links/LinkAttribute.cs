using System;
using Common.Application.Authorisation;
using Common.Setup.Infrastructure.Constants;

namespace Common.Api.Links
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class LinkAttribute : Attribute
    {
        public string LinkName { get; }

        public string UriName { get; }

        public HttpVerb HttpVerb { get; }

        public Type AuthorisationContextType { get; }
        
        public LinkAttribute(string linkName, string uriName, HttpVerb httpVerb)
        {
            LinkName = linkName;
            UriName = uriName;
            HttpVerb = httpVerb;
        }

        public LinkAttribute(string linkName, string uriName, HttpVerb httpVerb, Type authorisationContextType)
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