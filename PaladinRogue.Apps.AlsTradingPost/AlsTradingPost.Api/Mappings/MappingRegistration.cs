using AutoMapper;

namespace AlsTradingPost.Api.Mappings
{
    public class MappingRegistration
    {

        public static void RegisterMappers(IMapperConfigurationExpression configuration)
        {
             RegisterApiMappers(configuration);

             Setup.MappingRegistration.RegisterMappers(configuration);
        }

        public static void RegisterApiMappers(IMapperConfigurationExpression configuration)
        {
            configuration.AddProfile<AdminApiMappingProfile>();
        }
    }
}
