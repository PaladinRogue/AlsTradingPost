using System;
using System.Linq;
using Authentication.Domain.IdentityServices.Handlers;
using Authentication.Domain.IdentityServices.Interfaces;
using Authentication.Domain.IdentityServices.Models;
using Authentication.Domain.Models;
using Authentication.Persistence.Interfaces;
using AutoMapper;
using Common.Domain.DomainEvents.Interfaces;

namespace Authentication.Domain.IdentityServices
{
    public class IdentityQueryService : IIdentityQueryService
    {
        private readonly IMapper _mapper;
        private readonly IIdentityRepository _identityRepository;
        private readonly IDomainEventFactory _domainEventFactory;
	    private readonly IDomainEventDispatcher _domainEventDispatcher;

		public IdentityQueryService(IMapper mapper, IIdentityRepository identityRepository, IDomainEventFactory domainEventFactory, IDomainEventDispatcher domainEventDispatcher)
        {
            _identityRepository = identityRepository;
	        _domainEventFactory = domainEventFactory;
	        _domainEventDispatcher = domainEventDispatcher;
	        _mapper = mapper;
        }

        public IdentityProjection GetByAuthenticationId(string authenticationId)
        {
	        var identity = _identityRepository.Get().FirstOrDefault(entity => entity.AuthenticationId == authenticationId);

	        _domainEventFactory.Raise(new DomainEvent
	        {
				Test = "YEEAS"
	        });

	        _domainEventDispatcher.DispatchEvents();

			return _mapper.Map<Identity, IdentityProjection>(identity);
        }

        public IdentityProjection Get(Guid id)
        {
            return _mapper.Map<Identity, IdentityProjection>(_identityRepository.GetById(id));
        }
    }
}
