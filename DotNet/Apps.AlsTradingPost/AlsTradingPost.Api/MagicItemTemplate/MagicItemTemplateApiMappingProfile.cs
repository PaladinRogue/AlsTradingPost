using AlsTradingPost.Application.MagicItemTemplate.Models;

namespace AlsTradingPost.Api.MagicItemTemplate
{
    public class MagicItemTemplateApiMappingProfile : AutoMapper.Profile
    {
        public MagicItemTemplateApiMappingProfile()
        {
            CreateMap<MagicItemTemplateSummaryAdto, MagicItemTemplateSummaryResource>();
            CreateMap<MagicItemTemplatePagedCollectionAdto, MagicItemTemplates>();
        }
    }
}
