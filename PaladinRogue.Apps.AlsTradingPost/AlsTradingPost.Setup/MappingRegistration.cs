using AlsTradingPost.Application.Admin.Mappings;
using AlsTradingPost.Domain.AdminServices.Mappings;
using AutoMapper;
using Common.Domain.Mappings;

namespace AlsTradingPost.Setup
{
    public class MappingRegistration
    {

        public static void RegisterMappers(IMapperConfigurationExpression configuration)
        {
            RegisterApplicationMappers(configuration);
            RegisterDomainMappers(configuration);
        }

        public static void RegisterApplicationMappers(IMapperConfigurationExpression configuration)
        {
            configuration.AddProfile<AdminApplicationMappingProfile>();
        }

        public static void RegisterDomainMappers(IMapperConfigurationExpression configuration)
        {
            configuration.AddProfile<DomainMappingProfile>();

            configuration.AddProfile<AdminDomainMappingProfile>();
        }
    }
}
