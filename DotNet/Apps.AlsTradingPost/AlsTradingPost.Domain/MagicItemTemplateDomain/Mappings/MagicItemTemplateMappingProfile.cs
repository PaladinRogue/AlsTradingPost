using AlsTradingPost.Domain.MagicItemTemplateDomain.Models;
using AlsTradingPost.Domain.Models;
using AutoMapper;

namespace AlsTradingPost.Domain.MagicItemTemplateDomain.Mappings
{
    public class MagicItemTemplateMappingProfile : Profile
    {
        public MagicItemTemplateMappingProfile()
        {
            CreateMap<MagicItemTemplate, MagicItemTemplateSummaryProjection>();
        }
    }
}
