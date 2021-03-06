﻿using System;

namespace PaladinRogue.Library.Core.Api.Links
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SearchLinkAttribute : LinkAttribute
    {
        public SearchLinkAttribute(string uriName)
            : base(LinkType.Search, uriName, Setup.Infrastructure.Constants.HttpVerb.Get)
        {
        }
    }
}