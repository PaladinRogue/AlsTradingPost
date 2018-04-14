using System;
using AlsTradingPost.Domain.DomainEvents;
using AlsTradingPost.Domain.Persistence;
using AlsTradingPost.Domain.UserDomain.Interfaces;
using AlsTradingPost.Domain.UserDomain.Models;
using AutoMapper;
using Common.Domain.DomainEvents.Interfaces;
using Common.Domain.Exceptions;
using Common.Domain.Models;
using Microsoft.Extensions.Logging;

namespace AlsTradingPost.Domain.UserDomain
{
    public class UserCommandService : IUserCommandService
    {
        private readonly ILogger<UserCommandService> _logger;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IPendingDomainEventDirector _pendingDomainEventDirector;

        public UserCommandService(IMapper mapper,
            IUserRepository userRepository,
            ILogger<UserCommandService> logger,
            IPendingDomainEventDirector pendingDomainEventDirector)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
            _pendingDomainEventDirector = pendingDomainEventDirector;
        }

        public UserProjection Update(UpdateUserDdto entity)
        {
            Domain.Models.User user = null;
            try
            {
                user = _mapper.Map<UpdateUserDdto, Domain.Models.User>(entity);

                _userRepository.Update(user);

                _pendingDomainEventDirector.Add(UserUpdatedDomainEvent.Create(user));

                return _mapper.Map<Domain.Models.User, UserProjection>(_userRepository.GetById(entity.Id));
            }
            catch (ConcurrencyDomainException e)
            {
                _logger.LogCritical(e, "Unable to update user");
                throw;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to update user");
                throw new UpdateDomainException(user, e);
            }
        }

        public UserProjection Create(CreateUserDdto entity)
        {
            Domain.Models.User user = null;
            try
            {
                user = _mapper.Map(entity, EntityFactory.CreateEntity<Domain.Models.User>());

                _userRepository.Add(user);

                _pendingDomainEventDirector.Add(UserCreatedDomainEvent.Create(user));

                return _mapper.Map<Domain.Models.User, UserProjection>(_userRepository.GetById(user.Id));
            }
            catch (ConcurrencyDomainException e)
            {
                _logger.LogCritical(e, "Unable to create user");
                throw;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to create user");
                throw new UpdateDomainException(user, e);
            }
        }
    }
}
