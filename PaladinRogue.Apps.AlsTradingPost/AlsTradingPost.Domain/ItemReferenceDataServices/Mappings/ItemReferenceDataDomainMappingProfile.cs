using AlsTradingPost.Domain.Models;
using AutoMapper;

namespace AlsTradingPost.Domain.ItemReferenceDataServices.Mappings
{
    public class ItemReferenceDataDomainMappingProfile : Profile
    {
        public ItemReferenceDataDomainMappingProfile()
        {
            CreateMap<ItemReferenceData, ItemReferenceData>()
                .ForMember(x => x.Version, opts => opts.Ignore());
        }
    }
}
