﻿using Common.Api.Links;
using Common.Api.Pagination;
using Common.Setup.Infrastructure.Constants;

namespace AlsTradingPost.Api.MagicItemTemplate
{
    [SelfLink(RouteDictionary.MagicItemTemplateGet, HttpVerbs.Get)]
    public class MagicItemTemplatePagedCollectionResource : PagedCollectionResource<MagicItemTemplateSummaryResource>
    {
    }
}