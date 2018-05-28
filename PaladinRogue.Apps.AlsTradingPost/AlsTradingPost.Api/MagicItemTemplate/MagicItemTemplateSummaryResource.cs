using System;
using AlsTradingPost.Setup.Infrastructure.Routing;
using Common.Api.Links;
using Common.Api.Meta;
using Common.Api.Resources;
using Common.Setup.Infrastructure.Constants;

namespace AlsTradingPost.Api.MagicItemTemplate
{
    [SelfLink(RouteDictionary.MagicItemTemplateGetById, HttpVerbs.Get)]
    public class MagicItemTemplateSummaryResource : ISummaryResource
    {
        [Hidden]
        [ReadOnly]
        public Guid Id { get; set; }
        
        [Sortable]
        [ReadOnly]
        public string Name { get; set; }
    }
}
