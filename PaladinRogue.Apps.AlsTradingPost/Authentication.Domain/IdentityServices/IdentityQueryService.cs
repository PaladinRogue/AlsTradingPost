using System;
using Authentication.Domain.IdentityServices.Interfaces;
using Authentication.Domain.Models;
using Authentication.Domain.Persistence;

namespace Authentication.Domain.IdentityServices
{
	public class IdentityQueryService : IIdentityQueryService
	{
		private readonly IIdentityRepository _identityRepository;

		public IdentityQueryService(IIdentityRepository identityRepository)
		{
			_identityRepository = identityRepository;
		}

		public Identity GetByAuthenticationId(string authenticationId)
		{
			return _identityRepository.GetSingle(i => i.AuthenticationId == authenticationId);
		}

		public Identity GetById(Guid id)
		{
			return _identityRepository.GetById(id);
		}
	}
}
