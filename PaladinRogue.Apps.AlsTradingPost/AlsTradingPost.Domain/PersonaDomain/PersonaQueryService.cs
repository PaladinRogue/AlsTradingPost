using System;
using System.Collections.Generic;
using System.Linq;
using AlsTradingPost.Domain.Models.Interfaces;
using AlsTradingPost.Domain.Persistence;
using AlsTradingPost.Domain.PersonaDomain.Interfaces;
using AutoMapper;

namespace AlsTradingPost.Domain.PersonaDomain
{
    public class PersonaQueryService : IPersonaQueryService
    {
        private readonly ITraderRepository _traderRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;

        public PersonaQueryService(IMapper mapper,
            IAdminRepository adminRepository,
            ITraderRepository traderRepository)
        {
            _mapper = mapper;
            _adminRepository = adminRepository;
            _traderRepository = traderRepository;
        }

        public IEnumerable<IPersona> GetUserPersonas(Guid userid)
        {
            List<IPersona> personas = new List<IPersona>
            {
                _traderRepository.GetById(userid),
                _adminRepository.GetById(userid)
            };

            return personas.Where(p => p != null);
        }
    }
}
