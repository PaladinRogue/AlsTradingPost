using AlsTradingPost.Application.MagicItemTemplate.Models;
using AlsTradingPost.Domain.MagicItemTemplateDomain.Models;
using AutoMapper;

namespace AlsTradingPost.Application.MagicItemTemplate.Mappings
{
    public class MagicItemTemplateMappingProfile : Profile
    {
        public MagicItemTemplateMappingProfile()
        {
            CreateMap<MagicItemTemplateSummaryProjection, MagicItemTemplateSummaryAdto>();
        }
    }
}
