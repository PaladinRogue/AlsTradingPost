using System;
using System.Linq;
using AlsTradingPost.Domain.UserDomain.Interfaces;
using AlsTradingPost.Domain.UserDomain.Models;
using AlsTradingPost.Resources;
using AutoMapper;

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
            UserProjection existingUser = _userQueryService.GetByIdentityId(loginDdto.IdentityId);

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
            CreateUserDdto createUserDdto = _mapper.Map<LoginDdto, CreateUserDdto>(loginDdto);

            UserProjection userProjection = _userCommandService.Create(createUserDdto);

            return _mapper.Map<UserProjection, AuthenticatedUserProjection>(userProjection);
        }

        private AuthenticatedUserProjection ReturnLogin(LoginDdto loginDdto, UserProjection existingUser)
        {
            UpdateUserDdto existingUserDdto = _mapper.Map<UserProjection, UpdateUserDdto>(existingUser);
            UpdateUserDdto updateUserDdto = _mapper.Map(loginDdto, existingUserDdto);
            return _mapper.Map<UserProjection, AuthenticatedUserProjection>(_userCommandService.Update(updateUserDdto));
        }
    }
}