﻿using System;
using Common.Setup.Infrastructure.Constants;

namespace Common.Api.Links
{
    public class AuthorisationLink
    {
        private AuthorisationLink(HttpVerb httpVerb, Type authorisationContextType)
        {
            HttpVerb = httpVerb;
            AuthorisationContextType = authorisationContextType;
        }

        public HttpVerb HttpVerb { get; }

        public Type AuthorisationContextType { get; }

        public static AuthorisationLink Create(HttpVerb httpVerb, Type authorisationContextType)
        {
            return new AuthorisationLink(httpVerb, authorisationContextType);
        }
    }
}