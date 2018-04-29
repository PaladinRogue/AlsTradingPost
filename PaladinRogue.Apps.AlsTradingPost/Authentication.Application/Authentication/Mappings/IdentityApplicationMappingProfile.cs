using Authentication.Application.Authentication.Models;
using Authentication.Domain.IdentityServices.Models;
using AutoMapper;

namespace Authentication.Application.Authentication.Mappings
{
    public class AuthenticationApplicationMappingProfile : Profile
    {
        public AuthenticationApplicationMappingProfile()
        {
            CreateMap<LoginAdto, LoginDdto>();
        }
    }
}
