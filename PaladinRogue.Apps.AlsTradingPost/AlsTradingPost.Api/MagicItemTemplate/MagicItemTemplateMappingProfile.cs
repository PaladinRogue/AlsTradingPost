using AlsTradingPost.Application.MagicItemTemplate.Models;
using AutoMapper;

namespace AlsTradingPost.Api.MagicItemTemplate
{
    public class MagicItemTemplateMappingProfile : Profile
    {
        public MagicItemTemplateMappingProfile()
        {
            CreateMap<MagicItemTemplateSummaryAdto, MagicItemTemplateSummaryResource>();
            CreateMap<MagicItemTemplatePagedCollectionAdto, MagicItemTemplatePagedCollectionResource>();
        }
    }
}
