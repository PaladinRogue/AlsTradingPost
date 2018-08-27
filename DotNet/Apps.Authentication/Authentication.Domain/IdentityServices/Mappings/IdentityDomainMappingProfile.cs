using Authentication.Domain.IdentityServices.Models;
using Authentication.Domain.Models;
using AutoMapper;

namespace Authentication.Domain.IdentityServices.Mappings
{
    public class IdentityDomainMappingProfile : Profile
    {
        public IdentityDomainMappingProfile()
        {
            CreateMap<LoginDdto, Identity>();
            CreateMap<Identity, AuthenticatedIdentityProjection>();
        }
    }
}
