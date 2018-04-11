using System;
using AlsTradingPost.Domain.DomainEvents;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;
using AlsTradingPost.Domain.UserServices.Interfaces;
using AlsTradingPost.Domain.UserServices.Models;
using AutoMapper;
using Common.Domain.DomainEvents.Interfaces;
using Common.Domain.Exceptions;
using Common.Domain.Models;
using Microsoft.Extensions.Logging;

namespace AlsTradingPost.Domain.UserServices
{
    public class UserCommandService : IUserCommandService
    {
        private readonly ILogger<UserCommandService> _logger;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IPendingDomainEventContainer _pendingDomainEventContainer;

        public UserCommandService(IMapper mapper,
            IUserRepository userRepository,
            ILogger<UserCommandService> logger,
            IPendingDomainEventContainer pendingDomainEventContainer)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
            _pendingDomainEventContainer = pendingDomainEventContainer;
        }

        public UserProjection Create(CreateUserDdto entity)
        {
            try
            {
                User newUser = _mapper.Map(entity, EntityFactory.CreateEntity<User>());

                _userRepository.Add(newUser);

                _pendingDomainEventContainer.Add(UserCreatedDomainEvent.Create(newUser));

                return _mapper.Map<User, UserProjection>(_userRepository.GetById(newUser.Id));
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to create user");
                throw new DomainException("Unable to create user");
            }
        }

        public UserProjection Update(UpdateUserDdto entity)
        {
            try
            {
                User user = _mapper.Map<UpdateUserDdto, User>(entity);

                _userRepository.Update(user);

                _pendingDomainEventContainer.Add(UserCreatedDomainEvent.Create(user));

                return _mapper.Map<User, UserProjection>(_userRepository.GetById(entity.Id));
            }
            catch (ConcurrencyDomainException e)
            {
                _logger.LogCritical(e, "Unable to create user");
                throw;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to update user");
                throw new DomainException("Unable to update user");
            }
        }
    }
}
