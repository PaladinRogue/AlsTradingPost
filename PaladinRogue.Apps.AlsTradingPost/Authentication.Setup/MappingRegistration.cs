using Authentication.Application.Application.Mappings;
using Authentication.Application.Authentication.Mappings;
using Authentication.Domain.IdentityServices.Mappings;
using AutoMapper;
using Common.Authentication.Domain.SessionDomain.Mappings;
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
            configuration.AddProfile<AuthenticationApplicationMappingProfile>();
        }

        public static void RegisterDomainMappers(IMapperConfigurationExpression configuration)
        {
            configuration.AddProfile<DomainMappingProfile>();
            configuration.AddProfile<IdentityDomainMappingProfile>();
            configuration.AddProfile<SessionDomainMappingProfile>();
        }
    }
}
