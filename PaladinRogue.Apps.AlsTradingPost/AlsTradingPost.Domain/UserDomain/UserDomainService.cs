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

        public UserDomainService(
            IUserQueryService userQueryService,
            IUserCommandService userCommandService)
        {
            _userQueryService = userQueryService;
            _userCommandService = userCommandService;
        }

        public AuthenticatedUserProjection Login(LoginDdto loginDdto)
        {
            AuthenticatedUserProjection authenticatedUserProjection;
            UserProjection existingUser = _userQueryService.GetByIdentityId(loginDdto.IdentityId);

            if (existingUser == null)
            {
                CreateUserDdto createUserDdto = Mapper.Map<LoginDdto, CreateUserDdto>(loginDdto);

                authenticatedUserProjection =
                    Mapper.Map<UserProjection, AuthenticatedUserProjection>(_userCommandService.Create(createUserDdto));
            }
            else
            {
                UpdateUserDdto existingUserDdto = Mapper.Map<UserProjection, UpdateUserDdto>(existingUser);
                UpdateUserDdto updateUserDdto = Mapper.Map(loginDdto, existingUserDdto);
                authenticatedUserProjection =
                    Mapper.Map<UserProjection, AuthenticatedUserProjection>(_userCommandService.Update(updateUserDdto));
            }

            authenticatedUserProjection.Personas = PersonTypeMapper.GetPersonaFlags(
                _userQueryService.GetUserPersonas(authenticatedUserProjection.Id).Select(u => u.PersonaType).ToArray()
            );

            return authenticatedUserProjection;
        }
    }
}