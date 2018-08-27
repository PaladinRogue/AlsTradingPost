using Authentication.Domain.ApplicationServices.Models;
using Common.Messaging.Messages;
using AutoMapper;

namespace Authentication.Application.Application.Mappings
{
    public class ApplicationApplicationMappingProfile : Profile
    {
        public ApplicationApplicationMappingProfile()
        {
            CreateMap<ApplicationCreatedMessage, CreateApplicationDdto>();
            CreateMap<ApplicationCreatedMessage, ApplicationProjection>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<ApplicationProjection, UpdateApplicationDdto>();
        }
    }
}
