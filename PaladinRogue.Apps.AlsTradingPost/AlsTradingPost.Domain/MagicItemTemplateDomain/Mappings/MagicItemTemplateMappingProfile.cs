using AutoMapper;

namespace AlsTradingPost.Domain.MagicItemTemplateDomain.Mappings
{
    public class MagicItemTemplateMappingProfile : Profile
    {
        public MagicItemTemplateMappingProfile()
        {
            CreateMap<Domain.Models.MagicItemTemplate, Domain.Models.MagicItemTemplate>()
                .ForMember(x => x.Version, opts => opts.Ignore());
        }
    }
}
