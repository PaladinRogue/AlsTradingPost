using AlsTradingPost.Api.Controllers;
using AlsTradingPost.Application.ItemReferenceDataApplication.Models;
using AutoMapper;

namespace AlsTradingPost.Api.ItemReferenceData
{
    public class ItemReferenceDataApiMappingProfile : Profile
    {
        public ItemReferenceDataApiMappingProfile()
        {
            CreateMap<ItemReferenceDataSummaryAdto, ItemReferenceDataSummaryResource>();
            CreateMap<ItemReferenceDataPagedCollectionAdto, ItemReferenceDataPagedCollectionResource>();
        }
    }
}
