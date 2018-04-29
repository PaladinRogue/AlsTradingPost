using AlsTradingPost.Domain.Models;
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
            CreateMap<LoginDdto, UpdateUserDdto>();
            CreateMap<UserProjection, AuthenticatedUserProjection>();
            CreateMap<User, UserProjection>()
                .IncludeBase<IVersionedEntity, IVersionedProjection>();
            CreateMap<CreateUserDdto, User>();
            CreateMap<UpdateUserDdto, User>()
                .IncludeBase<IVersionedDdto, IVersionedEntity>();
        }
    }
}
