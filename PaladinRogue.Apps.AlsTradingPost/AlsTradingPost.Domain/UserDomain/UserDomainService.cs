using System.Linq;
using AlsTradingPost.Domain.PlayerDomain.Interfaces;
using AlsTradingPost.Domain.PlayerDomain.Models;
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
        private readonly IPlayerCommandService _playerCommandService;
        private readonly IMapper _mapper;

        public UserDomainService(
            IUserQueryService userQueryService,
            IUserCommandService userCommandService,
            IPlayerCommandService playerCommandService,
            IMapper mapper)
        {
            _userQueryService = userQueryService;
            _userCommandService = userCommandService;
            _playerCommandService = playerCommandService;
            _mapper = mapper;
        }

        public AuthenticatedUserProjection Login(LoginDdto loginDdto)
        {
            UserProjection existingUser = _userQueryService.GetByIdentityId(loginDdto.IdentityId);

            AuthenticatedUserProjection authenticatedUserProjection =
                existingUser == null ? FirstTimeLogin(loginDdto) : ReturnLogin(loginDdto, existingUser);

            authenticatedUserProjection.Personas = PersonTypeMapper.GetPersonaFlags(
                _userQueryService.GetUserPersonas(authenticatedUserProjection.Id).Select(u => u.PersonaType).ToArray()
            );

            return authenticatedUserProjection;
        }

        private AuthenticatedUserProjection FirstTimeLogin(LoginDdto loginDdto)
        {
            CreateUserDdto createUserDdto = _mapper.Map<LoginDdto, CreateUserDdto>(loginDdto);

            UserProjection userProjection = _userCommandService.Create(createUserDdto);

            _playerCommandService.Create(_mapper.Map<UserProjection, CreatePlayerDdto>(userProjection));

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