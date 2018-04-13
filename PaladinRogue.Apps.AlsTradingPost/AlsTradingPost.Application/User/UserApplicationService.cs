using System;
using System.Collections.Generic;
using AlsTradingPost.Application.User.Interfaces;
using AlsTradingPost.Application.User.Models;
using AlsTradingPost.Domain.UserServices.Interfaces;
using AlsTradingPost.Domain.UserServices.Models;
using AlsTradingPost.Resources;
using AutoMapper;
using Common.Domain.Exceptions;
using Common.Resources.Exceptions;
using Common.Resources.Providers.Interfaces;
using Microsoft.Extensions.Logging;

namespace AlsTradingPost.Application.User
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
            return _mapper.Map<UserProjection, UserAdto> (_userQueryService.Get(id));
        }

        public IList<UserSummaryAdto> GetAll()
        {
            return _mapper.Map<IList<UserSummaryProjection>, IList<UserSummaryAdto>>(_userQueryService.GetAll());
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
                throw new AppException(ExceptionType.Concurrency, e);
            }
        }
    }
}

