using AutoMapper;
using Common.Authentication.Domain.SessionDomain.Mappings;
using Common.Domain.Mappings;

namespace ApplicationManager.Setup
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
            configuration.AddProfile<SessionDomainMappingProfile>();
        }
    }
}
