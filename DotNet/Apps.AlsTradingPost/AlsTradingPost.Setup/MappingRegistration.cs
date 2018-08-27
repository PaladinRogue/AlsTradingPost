using AlsTradingPost.Application.Authentication.Mappings;
using AlsTradingPost.Application.Trader.Mappings;
using AlsTradingPost.Domain.AdminDomain.Mappings;
using AlsTradingPost.Domain.MagicItemTemplateDomain.Mappings;
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
            configuration.AddProfile<AuthenticationApplicationMappingProfile>();
            configuration.AddProfile<TraderApplicationMappingProfile>();
        }

        public static void RegisterDomainMappers(IMapperConfigurationExpression configuration)
        {
            configuration.AddProfile<DomainMappingProfile>();

            configuration.AddProfile<MagicItemTemplateMappingProfile>();
            configuration.AddProfile<AdminDomainMappingProfile>();
            configuration.AddProfile<UserDomainMappingProfile>();
            configuration.AddProfile<TraderDomainMappingProfile>();
        }
    }
}
