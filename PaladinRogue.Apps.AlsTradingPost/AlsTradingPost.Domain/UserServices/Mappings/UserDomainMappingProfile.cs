﻿using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.UserServices.Models;
using AutoMapper;
using Common.Domain.Concurrency.Interfaces;
using Common.Domain.Models.Interfaces;

namespace AlsTradingPost.Domain.UserServices.Mappings
{
    public class UserDomainMappingProfile : Profile
    {
        public UserDomainMappingProfile()
        {
            CreateMap<User, UserProjection>()
                .IncludeBase<IEntity, IVersionedProjection>();
            CreateMap<CreateUserDdto, User>();
            CreateMap<UpdateUserDdto, User>()
                .IncludeBase<IVersionedDdto, IEntity>();
        }
    }
}
