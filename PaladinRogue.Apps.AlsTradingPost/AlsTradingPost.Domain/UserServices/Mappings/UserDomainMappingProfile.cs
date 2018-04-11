using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.UserServices.Models;
using AutoMapper;
using Common.Domain.Interfaces;
using Common.Domain.Models.Interfaces;

namespace AlsTradingPost.Domain.UserServices.Mappings
{
    public class UserDomainMappingProfile : Profile
    {
        public UserDomainMappingProfile()
        {
            CreateMap<User, UserProjection>()
                .IncludeBase<IEntity, IVersionedProjection>();
            CreateMap<UpdateUserDdto, User>()
                .IncludeBase<IVersionedDdto, IEntity>();
        }
    }
}
