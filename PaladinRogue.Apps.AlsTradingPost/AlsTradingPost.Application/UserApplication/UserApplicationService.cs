using System;
using AlsTradingPost.Application.UserApplication.Interfaces;
using AlsTradingPost.Application.UserApplication.Models;
using AlsTradingPost.Domain.UserDomain.Interfaces;
using AlsTradingPost.Domain.UserDomain.Models;
using AlsTradingPost.Resources;
using AutoMapper;
using Common.Application.Exceptions;
using Common.Domain.Exceptions;
using Common.Resources.Concurrency.Interfaces;
using Microsoft.Extensions.Logging;
using ApplicationException = Common.Application.Exceptions.ApplicationException;

namespace AlsTradingPost.Application.UserApplication
{
    public class UserApplicationService : IUserApplicationService
    {
        private readonly ILogger<UserApplicationService> _logger;
        private readonly IUserCommandService _userCommandService;
        private readonly IUserQueryService _userQueryService;
        private readonly IMapper _mapper;
        private readonly ICurrentIdentityProvider _currentIdentityProvider;

        public UserApplicationService(ILogger<UserApplicationService> logger,
            IMapper mapper,
            IUserCommandService userCommandService,
            IUserQueryService userQueryService,
            ICurrentIdentityProvider currentIdentityProvider)
        {
            _mapper = mapper;
            _userCommandService = userCommandService;
            _userQueryService = userQueryService;
            _currentIdentityProvider = currentIdentityProvider;
            _logger = logger;
        }

        public UserAdto Get(Guid id)
        {
            return _mapper.Map<UserProjection, UserAdto>(_userQueryService.GetById(id));
        }

        public UserAdto FacebookUpdate(FacebookUpdateAdto user)
        {
            try
            {
                Guid identityId = _currentIdentityProvider.Id;
                UserProjection existingUser = _userQueryService.GetByIdentityId(identityId);

                UserProjection userProjection;
                if (existingUser == null)
                {
                    CreateUserDdto createUserDdto = _mapper.Map<FacebookUpdateAdto, CreateUserDdto>(user);

                    createUserDdto.Personas = Persona.Player;
                    createUserDdto.IdentityId = identityId;

                    userProjection = _userCommandService.Create(createUserDdto);
                }
                else
                {
                    UpdateUserDdto existingUserDdto = _mapper.Map<UserProjection, UpdateUserDdto>(existingUser);
                    UpdateUserDdto updateUserDdto = _mapper.Map(user, existingUserDdto);
                    userProjection = _userCommandService.Update(updateUserDdto);
                }

                return Mapper.Map<UserProjection, UserAdto>(userProjection);
            }
            catch (ConcurrencyDomainException e)
            {
                _logger.LogInformation(e, "Concurrency exception");
                throw new ApplicationException(ExceptionType.Concurrency, e);
            }
        }
    }
}

