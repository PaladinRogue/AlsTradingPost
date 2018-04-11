using System;
using System.Collections.Generic;
using AlsTradingPost.Application.User.Interfaces;
using AlsTradingPost.Application.User.Models;
using AlsTradingPost.Domain.UserServices.Interfaces;
using AlsTradingPost.Domain.UserServices.Models;
using AutoMapper;
using Common.Application;
using Common.Domain.ConcurrencyServices.Interfaces;
using Common.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace AlsTradingPost.Application.User
{
    public class UserApplicationService : IUserApplicationService
    {
        private readonly ILogger<UserApplicationService> _logger;
        private readonly IUserCommandService _userCommandService;
        private readonly IUserQueryService _userQueryService;
        private readonly IMapper _mapper;
        private readonly IConcurrencyQueryService<IUserQueryService> _concurrencyQueryService;

        public UserApplicationService(ILogger<UserApplicationService> logger,
            IMapper mapper,
            IUserCommandService userCommandService,
            IUserQueryService userQueryService,
            IConcurrencyQueryService<IUserQueryService> concurrencyQueryService)
        {
            _mapper = mapper;
            _userCommandService = userCommandService;
            _userQueryService = userQueryService;
            _logger = logger;
            _concurrencyQueryService = concurrencyQueryService;
        }

        public UserAdto Get(Guid id)
        {
            return _mapper.Map<UserProjection, UserAdto> (_userQueryService.Get(id));
        }

        public IList<UserSummaryAdto> GetAll()
        {
            return _mapper.Map<IList<UserSummaryProjection>, IList<UserSummaryAdto>>(_userQueryService.GetAll());
        }

        public UserAdto Update(UpdateUserAdto user)
        {
            try
            {
                _concurrencyQueryService.CheckConcurrency(user.Id, user.Version);

                UpdateUserDdto updatedUser = _mapper.Map<UpdateUserAdto, UpdateUserDdto>(user);

                return _mapper.Map<UserProjection, UserAdto>(_userCommandService.Update(updatedUser));
            }
            catch (ConcurrencyDomainException e)
            {
                _logger.LogInformation(e, "Concurrency exception");
                throw new AppException(ExceptionType.Concurrency, e);
            }
        }
    }
}

