using AlsTradingPost.Domain.AdminDomain.Models;
using Common.Domain.Services;
using Common.Domain.Services.Interfaces;

namespace AlsTradingPost.Domain.AdminDomain.Interfaces
{
    public interface IAdminCommandService : ICreateCommandService<CreateAdminDdto, AdminProjection>, IUpdateCommandService<UpdateAdminDdto, AdminProjection>
    {
    }
}
