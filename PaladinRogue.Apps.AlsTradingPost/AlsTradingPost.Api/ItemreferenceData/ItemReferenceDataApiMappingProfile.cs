using AlsTradingPost.Application.ItemReferenceDataApplication.Models;
using AutoMapper;

namespace AlsTradingPost.Api.ItemreferenceData
{
    public class ItemReferenceDataApiMappingProfile : Profile
    {
        public ItemReferenceDataApiMappingProfile()
        {
            CreateMap<ItemReferenceDataSummaryAdto, ItemReferenceDataSummaryResource>();
        }
    }
}
