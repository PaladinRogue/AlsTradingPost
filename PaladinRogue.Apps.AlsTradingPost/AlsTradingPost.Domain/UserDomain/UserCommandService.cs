using System;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;
using AlsTradingPost.Domain.UserDomain.Interfaces;
using AutoMapper;
using Common.Domain.Exceptions;
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

        public void Update(User entity)
        {
            try
            {
                _userRepository.Update(entity);
            }
            catch (ConcurrencyDomainException e)
            {
                _logger.LogCritical(e, "Unable to update user");
                throw;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to update user");
                throw new UpdateDomainException(entity, e);
            }
        }

        public Guid Create(User entity)
        {
            try
            {
                _userRepository.Add(entity);

                return entity.Id;
            }
            catch (ConcurrencyDomainException e)
            {
                _logger.LogCritical(e, "Unable to create user");
                throw;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to create user");
                throw new CreateDomainException(entity, e);
            }
        }
    }
}
