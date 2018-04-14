using AlsTradingPost.Application.Admin.Mappings;
using AlsTradingPost.Application.User.Mappings;
using AlsTradingPost.Domain.AdminServices.Mappings;
using AlsTradingPost.Domain.ItemReferenceDataServices.Mappings;
using AlsTradingPost.Domain.UserServices.Mappings;
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
            configuration.AddProfile<UserApplicationMappingProfile>();
        }

        public static void RegisterDomainMappers(IMapperConfigurationExpression configuration)
        {
            configuration.AddProfile<DomainMappingProfile>();

            configuration.AddProfile<ItemReferenceDataDomainMappingProfile>();
            configuration.AddProfile<AdminDomainMappingProfile>();
            configuration.AddProfile<UserDomainMappingProfile>();
        }
    }
}
