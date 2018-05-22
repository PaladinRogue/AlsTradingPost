﻿using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.TraderDomain.Models;
using AutoMapper;
using Common.Domain.Concurrency.Interfaces;
using Common.Domain.Models.Interfaces;

namespace AlsTradingPost.Domain.TraderDomain.Mappings
{
    public class TraderDomainMappingProfile : Profile
    {
        public TraderDomainMappingProfile()
        {
            CreateMap<CreateTraderDdto, Trader>();
            CreateMap<Trader, TraderProjection>()
                .IncludeBase<IVersionedEntity, IVersionedProjection>();
            CreateMap<RegisterTraderDdto, CreateTraderDdto>()
                .ForMember(d => d.Id, opts => opts.MapFrom(s => s.UserId));
            CreateMap<TraderProjection, RegisteredTraderProjection>();
            CreateMap<UpdateTraderDdto, Trader>()
                .IncludeBase<IVersionedDdto, IVersionedEntity>();
        }
    }
}