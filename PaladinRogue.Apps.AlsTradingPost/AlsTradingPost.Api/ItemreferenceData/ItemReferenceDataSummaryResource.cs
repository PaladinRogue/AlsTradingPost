using System;
using Common.Api.ResourceFormatter.Attributes;

namespace AlsTradingPost.Api.ItemReferenceData
{
    public class ItemReferenceDataSummaryResource
    {
        public Guid Id { get; set; }
        [Sortable]
        public string Name { get; set; }
    }
}
