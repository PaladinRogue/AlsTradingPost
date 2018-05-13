using System;
using System.Collections.Generic;
using System.Linq;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Models.Interfaces;
using AlsTradingPost.Domain.Persistence;
using AlsTradingPost.Domain.UserDomain.Interfaces;
using AlsTradingPost.Domain.UserDomain.Models;
using AutoMapper;

namespace AlsTradingPost.Domain.UserDomain
{
    public class UserQueryService : IUserQueryService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITraderRepository _traderRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;

        public UserQueryService(
            IUserRepository userRepository,
            ITraderRepository traderRepository,
            IAdminRepository adminRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _traderRepository = traderRepository;
            _adminRepository = adminRepository;
            _mapper = mapper;
        }

        public UserProjection GetByIdentityId(Guid identityId)
        {
            return _mapper.Map<User, UserProjection>(_userRepository.GetSingle(u => u.IdentityId == identityId));
        }

        public IEnumerable<UserPersonaProjection> GetUserPersonas(Guid userid)
        {
            List<IPersona> personas = new List<IPersona>
            {
                _traderRepository.GetById(userid),
                _adminRepository.GetById(userid)
            };
            
            return _mapper.Map<IEnumerable<IPersona>, IEnumerable<UserPersonaProjection>>(personas.Where(p => p != null));
        }
    }
}
