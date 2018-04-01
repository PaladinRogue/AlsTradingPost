using System;
using System.Linq;
using Authentication.Domain.IdentityServices.Interfaces;
using Authentication.Domain.IdentityServices.Models;
using Authentication.Domain.Models;
using Authentication.Persistence.Interfaces;
using AutoMapper;

namespace Authentication.Domain.IdentityServices
{
    public class IdentityQueryService : IIdentityQueryService
    {
        private readonly IMapper _mapper;
        private readonly IIdentityRepository _identityRepository;

		public IdentityQueryService(IMapper mapper, IIdentityRepository identityRepository)
        {
            _identityRepository = identityRepository;
	        _mapper = mapper;
        }

        public IdentityProjection GetByAuthenticationId(string authenticationId)
        {
	        var identity = _identityRepository.Get().FirstOrDefault(entity => entity.AuthenticationId == authenticationId);

			return _mapper.Map<Identity, IdentityProjection>(identity);
        }

        public IdentityProjection Get(Guid id)
        {
            return _mapper.Map<Identity, IdentityProjection>(_identityRepository.GetById(id));
        }
    }
}
