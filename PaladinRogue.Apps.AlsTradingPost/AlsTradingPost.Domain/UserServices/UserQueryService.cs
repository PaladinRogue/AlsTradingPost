using System;
using System.Collections.Generic;
using System.Linq;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;
using AlsTradingPost.Domain.UserServices.Interfaces;
using AlsTradingPost.Domain.UserServices.Models;
using AutoMapper;

namespace AlsTradingPost.Domain.UserServices
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
            return _mapper.Map<User, UserProjection>(_userRepository.GetById(id));
        }

        public IList<UserSummaryProjection> GetAll()
        {
            return _mapper.Map<IList<User>, IList<UserSummaryProjection>>(_userRepository.Get().ToList());
        }
    }
}
