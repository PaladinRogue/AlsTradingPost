using System;
using System.Collections.Generic;
using System.Linq;
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

        public UserProjection Get(Guid id)
        {
            return _mapper.Map<Domain.Models.User, UserProjection>(_userRepository.GetById(id));
        }

        public IList<UserSummaryProjection> GetAll()
        {
            return _mapper.Map<IList<Domain.Models.User>, IList<UserSummaryProjection>>(_userRepository.Get().ToList<Domain.Models.User>());
        }

        public UserProjection GetByIdentityId(Guid identityId)
        {
            return _mapper.Map<Domain.Models.User, UserProjection>(_userRepository.GetSingle(u => u.IdentityId == identityId));
        }
    }
}
