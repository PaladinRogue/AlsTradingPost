using System;
using Authentication.Domain.IdentityServices.Interfaces;
using Authentication.Domain.IdentityServices.Models;
using Authentication.Domain.Models;
using Authentication.Domain.Persistence;
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
			return _mapper.Map<Identity, IdentityProjection>(_identityRepository.GetSingle(i => i.AuthenticationId == authenticationId));
		}

		public IdentityProjection GetById(Guid id)
		{
			return _mapper.Map<Identity, IdentityProjection>(_identityRepository.GetById(id));
		}
	}
}
