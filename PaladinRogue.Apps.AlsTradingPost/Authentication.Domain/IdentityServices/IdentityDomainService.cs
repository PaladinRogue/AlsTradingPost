using Authentication.Domain.IdentityServices.Interfaces;
using Authentication.Domain.IdentityServices.Models;
using AutoMapper;

namespace Authentication.Domain.IdentityServices
{
    public class IdentityDomainService : IIdentityDomainService
    {
        private readonly IIdentityCommandService _identityCommandService;
        private readonly IIdentityQueryService _identityQueryService;
        
        public IdentityDomainService(
            IIdentityQueryService identityQueryService,
            IIdentityCommandService identityCommandService)
        {
            _identityQueryService = identityQueryService;
            _identityCommandService = identityCommandService;
        }

        public IdentityProjection Login(LoginDdto loginDdto)
        {
            return _identityQueryService.GetByAuthenticationId(loginDdto.AuthenticationId) ??
                   _identityCommandService.Create(Mapper.Map<LoginDdto, CreateIdentityDdto>(loginDdto));
        }
    }
}