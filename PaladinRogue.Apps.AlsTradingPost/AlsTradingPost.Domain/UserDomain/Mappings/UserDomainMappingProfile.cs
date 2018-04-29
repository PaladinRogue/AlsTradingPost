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
            CreateMap<Domain.Models.User, UserProjection>()
                .IncludeBase<IVersionedEntity, IVersionedProjection>();
            CreateMap<CreateUserDdto, Domain.Models.User>();
            CreateMap<UpdateUserDdto, Domain.Models.User>()
                .IncludeBase<IVersionedDdto, IVersionedEntity>();
        }
    }
}
