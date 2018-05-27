using System;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;
using AlsTradingPost.Domain.UserDomain.Interfaces;

namespace AlsTradingPost.Domain.UserDomain
{
    public class UserQueryService : IUserQueryService
    {
        private readonly IUserRepository _userRepository;

        public UserQueryService(
            IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool CheckExists(Guid id)
        {
            return _userRepository.CheckExists(id);
        }

        public User GetByIdentityId(Guid identityId)
        {
            return _userRepository.GetSingle(u => u.IdentityId == identityId);
        }

        public User GetById(Guid id)
        {
            return _userRepository.GetById(id);
        }
    }
}
