using AlsTradingPost.Application.Admin.Mappings;
using AlsTradingPost.Application.Authentication.Mappings;
using AlsTradingPost.Domain.AdminDomain.Mappings;
using AlsTradingPost.Domain.ItemReferenceDataDomain.Mappings;
using AlsTradingPost.Domain.TraderDomain.Mappings;
using AlsTradingPost.Domain.UserDomain.Mappings;
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
            configuration.AddProfile<AuthenticationApplicationMappingProfile>();
        }

        public static void RegisterDomainMappers(IMapperConfigurationExpression configuration)
        {
            configuration.AddProfile<DomainMappingProfile>();

            configuration.AddProfile<ItemReferenceDataDomainMappingProfile>();
            configuration.AddProfile<AdminDomainMappingProfile>();
            configuration.AddProfile<UserDomainMappingProfile>();
            configuration.AddProfile<TraderDomainMappingProfile>();
        }
    }
}
