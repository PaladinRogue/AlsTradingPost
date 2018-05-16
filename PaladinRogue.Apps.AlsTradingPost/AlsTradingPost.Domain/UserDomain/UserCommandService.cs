using System;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;
using AlsTradingPost.Domain.UserDomain.Interfaces;
using AlsTradingPost.Domain.UserDomain.Models;
using AutoMapper;
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

        public UserCommandService(IMapper mapper,
            IUserRepository userRepository,
            ILogger<UserCommandService> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public UserProjection Update(UpdateUserDdto entity)
        {
            User user = null;
            try
            {
                user = _mapper.Map<UpdateUserDdto, User>(entity);

                _userRepository.Update(user);

                return _mapper.Map<User, UserProjection>(_userRepository.GetById(entity.Id));
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
            User user = null;
            try
            {
                user = _mapper.Map(entity, AggregateFactory.CreateRoot<User>());

                _userRepository.Add(user);

                return _mapper.Map<User, UserProjection>(_userRepository.GetById(user.Id));
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
