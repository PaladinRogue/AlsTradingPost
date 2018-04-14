using AlsTradingPost.Api.Admin;
using AlsTradingPost.Api.FacebookAuth;
using AlsTradingPost.Api.ItemreferenceData;
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
            configuration.AddProfile<FacebookAuthApiMappingProfile>();
            configuration.AddProfile<ItemReferenceDataApiMappingProfile>();
        }
    }
}
