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
		private readonly IIdentityRepository _identityRepository;

		public IdentityQueryService(IIdentityRepository identityRepository)
		{
			_identityRepository = identityRepository;
		}

		public IdentityProjection GetByAuthenticationId(string authenticationId)
		{
			return Mapper.Map<Identity, IdentityProjection>(_identityRepository.GetSingle(i => i.AuthenticationId == authenticationId));
		}

		public IdentityProjection GetById(Guid id)
		{
			return Mapper.Map<Identity, IdentityProjection>(_identityRepository.GetById(id));
		}
	}
}
