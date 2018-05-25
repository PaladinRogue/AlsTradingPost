﻿using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Models.Interfaces;
using AlsTradingPost.Domain.UserDomain.Models;
using AutoMapper;
using Common.Domain.Concurrency.Interfaces;
using Common.Domain.Models.Interfaces;

namespace AlsTradingPost.Domain.UserDomain.Mappings
{
    public class UserDomainMappingProfile : Profile
    {
        public UserDomainMappingProfile()
        {
            CreateMap<LoginDdto, User>();
            CreateMap<User, AuthenticatedUserProjection>()
                .IncludeBase<IVersionedEntity, IVersionedProjection>();
            CreateMap<IPersona, UserPersonaProjection>()
                .ForMember(p => p.PersonaType, opts => opts.MapFrom(p => p.TypeDiscriminator));
        }
    }
}
