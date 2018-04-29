using AlsTradingPost.Application.ItemReferenceData.Models;
using AlsTradingPost.Domain.ItemReferenceDataDomain.Models;
using AutoMapper;

namespace AlsTradingPost.Application.ItemReferenceData.Mappings
{
    public class ItemReferenceDataApplicationMappingProfile : Profile
    {
        public ItemReferenceDataApplicationMappingProfile()
        {
            CreateMap<ItemReferenceDataSummaryProjection, ItemReferenceDataSummaryAdto>();
        }
    }
}
