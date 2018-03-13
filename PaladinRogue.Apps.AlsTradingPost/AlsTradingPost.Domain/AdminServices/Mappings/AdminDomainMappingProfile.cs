using AlsTradingPost.Domain.AdminServices.Models;
using AlsTradingPost.Domain.Models;
using AutoMapper;
using Common.Domain.Interfaces;
using Common.Domain.Models.Interfaces;

namespace AlsTradingPost.Domain.AdminServices.Mappings
{
    public class AdminDomainMappingProfile : Profile
    {
        public AdminDomainMappingProfile()
        {
            CreateMap<Admin, AdminProjection>()
                .IncludeBase<IEntity, IVersionedProjection>();
            CreateMap<AdminDdto, Admin>();
        }
    }
}
