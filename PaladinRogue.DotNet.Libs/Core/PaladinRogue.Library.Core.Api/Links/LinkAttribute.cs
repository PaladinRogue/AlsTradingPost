﻿using System;
using PaladinRogue.Library.Authorisation.Common.Contexts;
using PaladinRogue.Library.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Library.Core.Api.Links
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class LinkAttribute : Attribute
    {
        public string LinkName { get; }

        public string UriName { get; }

        public HttpVerb HttpVerb { get; }

        public Type AuthorisationContextType { get; }

        public LinkAttribute(string linkName, string uriName, HttpVerb httpVerb, Type authorisationContextType = null)
        {
            LinkName = linkName;
            UriName = uriName;
            HttpVerb = httpVerb;

            if (authorisationContextType != null && !typeof(IAuthorisationContext).IsAssignableFrom(authorisationContextType))
            {
                throw new ArgumentOutOfRangeException(nameof(authorisationContextType));
            }

            AuthorisationContextType = authorisationContextType;
        }
    }
}