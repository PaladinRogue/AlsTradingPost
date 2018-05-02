using Authentication.Domain.IdentityServices.Models;
using Authentication.Domain.Models;
using AutoMapper;

namespace Authentication.Domain.IdentityServices.Mappings
{
    public class IdentityDomainMappingProfile : Profile
    {
        public IdentityDomainMappingProfile()
        {
            CreateMap<LoginDdto, CreateIdentityDdto>();
            CreateMap<IdentityProjection, LoginIdentityProjection>();
            CreateMap<Identity, IdentityProjection>();
            CreateMap<CreateIdentityDdto, Identity>();
        }
    }
}
