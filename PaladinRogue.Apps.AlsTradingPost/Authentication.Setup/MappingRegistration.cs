using Authentication.Application.Application.Mappings;
using Authentication.Application.Identity.Mappings;
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
            configuration.AddProfile<ApplicationApplicationMappingProfile>();
            configuration.AddProfile<IdentityApplicationMappingProfile>();
        }

        public static void RegisterDomainMappers(IMapperConfigurationExpression configuration)
        {
            configuration.AddProfile<DomainMappingProfile>();
        }
    }
}
