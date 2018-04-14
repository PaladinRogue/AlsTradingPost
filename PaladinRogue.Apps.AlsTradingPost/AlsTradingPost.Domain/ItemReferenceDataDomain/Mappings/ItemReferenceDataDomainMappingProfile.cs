using AutoMapper;

namespace AlsTradingPost.Domain.ItemReferenceDataDomain.Mappings
{
    public class ItemReferenceDataDomainMappingProfile : Profile
    {
        public ItemReferenceDataDomainMappingProfile()
        {
            CreateMap<Domain.Models.ItemReferenceData, Domain.Models.ItemReferenceData>()
                .ForMember(x => x.Version, opts => opts.Ignore());
        }
    }
}
