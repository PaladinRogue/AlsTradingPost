using System;
using AlsTradingPost.Domain.Persistence;
using AlsTradingPost.Domain.UserDomain.Interfaces;
using AlsTradingPost.Domain.UserDomain.Models;
using AutoMapper;

namespace AlsTradingPost.Domain.UserDomain
{
    public class UserQueryService : IUserQueryService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserQueryService(IMapper mapper, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public UserProjection GetById(Guid id)
        {
            return _mapper.Map<Domain.Models.User, UserProjection>(_userRepository.GetById(id));
        }

        public UserProjection GetByIdentityId(Guid identityId)
        {
            return _mapper.Map<Domain.Models.User, UserProjection>(_userRepository.GetSingle(u => u.IdentityId == identityId));
        }
    }
}
