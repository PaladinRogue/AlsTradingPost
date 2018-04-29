using AlsTradingPost.Domain.UserDomain.Interfaces;
using AlsTradingPost.Domain.UserDomain.Models;
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

        public UserProjection Login(LoginDdto loginDdto)
        {
            UserProjection existingUser = _userQueryService.GetByIdentityId(loginDdto.IdentityId);

            if (existingUser == null)
            {
                CreateUserDdto createUserDdto = Mapper.Map<LoginDdto, CreateUserDdto>(loginDdto);

                return _userCommandService.Create(createUserDdto);
            }

            UpdateUserDdto existingUserDdto = Mapper.Map<UserProjection, UpdateUserDdto>(existingUser);
            UpdateUserDdto updateUserDdto = Mapper.Map(loginDdto, existingUserDdto);
            return _userCommandService.Update(updateUserDdto);
        }
    }
}