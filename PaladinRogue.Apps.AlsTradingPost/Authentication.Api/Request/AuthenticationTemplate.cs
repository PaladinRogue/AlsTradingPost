﻿using Common.Api.Links;
using Common.Api.Resources;
using Common.Api.Validation.Attributes;
using Common.Setup.Infrastructure.Constants;

namespace Authentication.Api.Request
{
    [Link(LinkDictionary.AuthenticationFacebook, RouteDictionary.AuthenticationFacebook, HttpVerbs.Post)]
    public class AuthenticationTemplate : ITemplate
    {
        [Required]
        public string AccessToken { get; set; }
    }
}