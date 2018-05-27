using System;
using System.Collections.Generic;
using AlsTradingPost.Domain.Models.Interfaces;
using AlsTradingPost.Domain.PersonaDomain.Interfaces;
using AlsTradingPost.Domain.PersonaDomain.Models;
using AutoMapper;

namespace AlsTradingPost.Domain.PersonaDomain
{
    public class PersonaDomainService : IPersonaDomainService
    {
        private readonly IPersonaQueryService _personaQueryService;
        private readonly IMapper _mapper;

        public PersonaDomainService(IMapper mapper,
            IPersonaQueryService personaQueryService)
        {
            _mapper = mapper;
            _personaQueryService = personaQueryService;
        }

        public IEnumerable<PersonaProjection> GetUserPersonas(Guid userid)
        {
            return _mapper.Map<IEnumerable<IPersona>, IEnumerable<PersonaProjection>>(_personaQueryService.GetUserPersonas(userid));
        }
    }
}
