using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Models.Interfaces;
using AlsTradingPost.Domain.PlayerDomain.Models;
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
            CreateMap<UserProjection, CreatePlayerDdto>();
            CreateMap<LoginDdto, UpdateUserDdto>();
            CreateMap<UserProjection, AuthenticatedUserProjection>();
            CreateMap<IPersona, UserPersonaProjection>()
                .ForMember(p => p.PersonaType, opts => opts.MapFrom(p => p.TypeDiscriminator));
            CreateMap<User, UserProjection>()
                .IncludeBase<IVersionedEntity, IVersionedProjection>();
            CreateMap<CreateUserDdto, User>();
            CreateMap<UpdateUserDdto, User>()
                .IncludeBase<IVersionedDdto, IVersionedEntity>();
        }
    }
}
