using System;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.UserDomain.Interfaces;
using AlsTradingPost.Domain.UserDomain.Models;
using AutoMapper;
using Common.Domain.Models;
using Common.Domain.Services.Command;
using Common.Domain.Services.Query;

namespace AlsTradingPost.Domain.UserDomain
{
    public class UserDomainService : IUserDomainService
    {
        private readonly IQueryService<User> _userQueryService;
        private readonly ICommandService<User> _userCommandService;
        private readonly IMapper _mapper;

        public UserDomainService(
            IQueryService<User> userQueryService,
            ICommandService<User> userCommandService,
            IMapper mapper)
        {
            _userQueryService = userQueryService;
            _userCommandService = userCommandService;
            _mapper = mapper;
        }

        public AuthenticatedUserProjection Login(LoginDdto loginDdto)
        {
            User existingUser = _userQueryService.GetSingle(u => u.IdentityId == loginDdto.IdentityId);

            AuthenticatedUserProjection authenticatedUserProjection =
                existingUser == null ? FirstTimeLogin(loginDdto) : ReturnLogin(loginDdto, existingUser);

            return authenticatedUserProjection;
        }

        private AuthenticatedUserProjection FirstTimeLogin(LoginDdto loginDdto)
        {
            User newUser = _mapper.Map(loginDdto, AggregateFactory.CreateRoot<User>());

            Guid userId = _userCommandService.Create(newUser);

            return _mapper.Map<User, AuthenticatedUserProjection>(_userQueryService.GetById(userId));
        }

        private AuthenticatedUserProjection ReturnLogin(LoginDdto loginDdto, User existingUser)
        {
            User user = _mapper.Map(loginDdto, existingUser);

            _userCommandService.Update(user);
            
            return _mapper.Map<User, AuthenticatedUserProjection>(_userQueryService.GetById(user.Id));
        }
    }
}