using AutoMapper;
using Common.Domain.Mappings;

namespace Authentication.Setup
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
        }

        public static void RegisterDomainMappers(IMapperConfigurationExpression configuration)
        {
            configuration.AddProfile<DomainMappingProfile>();
        }
    }
}
