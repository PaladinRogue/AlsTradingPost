using Authentication.Application.Identity.Models;
using Authentication.Domain.IdentityServices.Models;
using AutoMapper;

namespace Authentication.Application.Identity.Mappings
{
    public class IdentityApplicationMappingProfile : Profile
    {
        public IdentityApplicationMappingProfile()
        {
            CreateMap<IdentityProjection, IdentityAdto>();
            CreateMap<GetIdentityAdto, CreateIdentityDdto>();
        }
    }
}
