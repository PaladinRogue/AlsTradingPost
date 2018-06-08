using AlsTradingPost.Application.MagicItemTemplate.Models;
using AutoMapper;

namespace AlsTradingPost.Api.MagicItemTemplate
{
    public class MagicItemTemplateApiMappingProfile : Profile
    {
        public MagicItemTemplateApiMappingProfile()
        {
            CreateMap<MagicItemTemplateSummaryAdto, MagicItemTemplateSummaryResource>();
            CreateMap<MagicItemTemplatePagedCollectionAdto, MagicItemTemplates>();
        }
    }
}
