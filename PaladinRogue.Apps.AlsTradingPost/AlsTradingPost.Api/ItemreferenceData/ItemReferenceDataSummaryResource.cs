using System;
using Common.Api.Builders.Resource.Attributes;
using Common.Api.Builders.Template.Attributes;

namespace AlsTradingPost.Api.ItemReferenceData
{
    public class ItemReferenceDataSummaryResource
    {
        public Guid Id { get; set; }
        [Sortable]
        public string Name { get; set; }
    }
}
