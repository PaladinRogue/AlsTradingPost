using System;
using Authentication.Domain.IdentityServices.Interfaces;
using Authentication.Domain.IdentityServices.Models;
using Authentication.Domain.Models;
using AutoMapper;
using Common.Domain.Models;

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

        public AuthenticatedIdentityProjection Login(LoginDdto loginDdto)
        {
            Identity identity = _identityQueryService.GetByAuthenticationId(loginDdto.AuthenticationId);

            if (identity == null)
            {
                Guid identityId = _identityCommandService.Create(Mapper.Map(loginDdto, AggregateFactory.CreateRoot<Identity>()));

                identity = _identityQueryService.GetById(identityId);
            }

            return _mapper.Map<Identity, AuthenticatedIdentityProjection>(identity);
        }
    }
}