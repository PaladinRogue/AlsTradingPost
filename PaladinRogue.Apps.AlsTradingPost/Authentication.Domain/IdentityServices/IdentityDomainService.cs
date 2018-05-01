using Authentication.Domain.IdentityServices.Interfaces;
using Authentication.Domain.IdentityServices.Models;
using AutoMapper;

namespace Authentication.Domain.IdentityServices
{
    public class IdentityDomainService : IIdentityDomainService
    {
        private readonly IIdentityCommandService _identityCommandService;
        private readonly IIdentityQueryService _identityQueryService;
        private readonly IMapper _mapper;
        
        public IdentityDomainService(
            IIdentityQueryService identityQueryService,
            IIdentityCommandService identityCommandService,
            IMapper mapper)
        {
            _identityQueryService = identityQueryService;
            _identityCommandService = identityCommandService;
            _mapper = mapper;
        }

        public LoginIdentityProjection Login(LoginDdto loginDdto)
        {
            IdentityProjection identityProjection =
                _identityQueryService.GetByAuthenticationId(loginDdto.AuthenticationId) ??
                _identityCommandService.Create(Mapper.Map<LoginDdto, CreateIdentityDdto>(loginDdto));
            
            return _mapper.Map<IdentityProjection, LoginIdentityProjection>(identityProjection);
        }
    }
}