using System;
using System.Linq;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.UserDomain.Interfaces;
using AlsTradingPost.Domain.UserDomain.Models;
using AlsTradingPost.Resources;
using AutoMapper;
using Common.Domain.Models;

namespace AlsTradingPost.Domain.UserDomain
{
    public class UserDomainService : IUserDomainService
    {
        private readonly IUserQueryService _userQueryService;
        private readonly IUserCommandService _userCommandService;
        private readonly IMapper _mapper;

        public UserDomainService(
            IUserQueryService userQueryService,
            IUserCommandService userCommandService,
            IMapper mapper)
        {
            _userQueryService = userQueryService;
            _userCommandService = userCommandService;
            _mapper = mapper;
        }

        public AuthenticatedUserProjection Login(LoginDdto loginDdto)
        {
            User existingUser = _userQueryService.GetByIdentityId(loginDdto.IdentityId);

            AuthenticatedUserProjection authenticatedUserProjection =
                existingUser == null ? FirstTimeLogin(loginDdto) : ReturnLogin(loginDdto, existingUser);

            authenticatedUserProjection.Personas = GetUserPersonaFlags(authenticatedUserProjection.Id);

            return authenticatedUserProjection;
        }

        public PersonaFlags GetUserPersonaFlags(Guid userId)
        {
            return PersonTypeMapper.GetPersonaFlags(
                _userQueryService.GetUserPersonas(userId).Select(u => u.PersonaType).ToArray()
            );
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