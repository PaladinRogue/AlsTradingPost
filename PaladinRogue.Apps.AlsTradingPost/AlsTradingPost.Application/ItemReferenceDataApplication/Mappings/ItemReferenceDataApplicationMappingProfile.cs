using AlsTradingPost.Application.ItemReferenceDataApplication.Models;
using AlsTradingPost.Domain.ItemReferenceDataDomain.Models;
using AutoMapper;

namespace AlsTradingPost.Application.ItemReferenceDataApplication.Mappings
{
    public class ItemReferenceDataApplicationMappingProfile : Profile
    {
        public ItemReferenceDataApplicationMappingProfile()
        {
            CreateMap<ItemReferenceDataSummaryProjection, ItemReferenceDataSummaryAdto>();
        }
    }
}
