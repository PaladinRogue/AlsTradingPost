using AlsTradingPost.Api.Admin;
using AlsTradingPost.Api.Authentication;
using AlsTradingPost.Api.ItemReferenceData;
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
            configuration.AddProfile<AuthenticationMappingProfile>();
            configuration.AddProfile<ItemReferenceDataApiMappingProfile>();
        }
    }
}
