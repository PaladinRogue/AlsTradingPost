using System;
using System.Collections.Generic;
using System.Linq;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Models.Interfaces;
using AlsTradingPost.Domain.PersonaDomain.Interfaces;
using AlsTradingPost.Domain.PersonaDomain.Models;
using AutoMapper;
using Common.Domain.Services.Query;

namespace AlsTradingPost.Domain.PersonaDomain
{
    public class PersonaDomainService : IPersonaDomainService
    {
        private readonly IQueryService<Trader> _traderQueryService;
        private readonly IQueryService<Admin> _adminQueryService;
        private readonly IMapper _mapper;

        public PersonaDomainService(IMapper mapper,
            IQueryService<Trader> traderQueryService,
            IQueryService<Admin> adminQueryService)
        {
            _mapper = mapper;
            _traderQueryService = traderQueryService;
            _adminQueryService = adminQueryService;
        }

        public IEnumerable<PersonaProjection> GetUserPersonas(Guid userid)
        {
            List<IPersona> personas = new List<IPersona>
            {
                _traderQueryService.GetById(userid),
                _adminQueryService.GetById(userid)
            };

            return _mapper.Map<IEnumerable<IPersona>, IEnumerable<PersonaProjection>>(personas.Where(p => p != null));
        }
    }
}
