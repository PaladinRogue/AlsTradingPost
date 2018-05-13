using AlsTradingPost.Api.Admin;
using AlsTradingPost.Api.Authentication;
using AlsTradingPost.Api.MagicItemTemplate;
using AlsTradingPost.Api.Trader;
using AutoMapper;

namespace AlsTradingPost.Api
{
    public class MappingRegistration
    {
        public static void RegisterMappers(IMapperConfigurationExpression configuration)
        {
            Setup.MappingRegistration.RegisterMappers(configuration);

            RegisterApiMappers(configuration);
        }

        public static void RegisterApiMappers(IMapperConfigurationExpression configuration)
        {
            configuration.AddProfile<AdminApiMappingProfile>();
            configuration.AddProfile<MagicItemTemplateApiMappingProfile>();
            configuration.AddProfile<TraderApiMappingProfile>();
        }
    }
}
