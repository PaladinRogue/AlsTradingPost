using AlsTradingPost.Domain.AdminServices.Models;
using AlsTradingPost.Domain.Interfaces;

namespace AlsTradingPost.Domain.AdminServices.Interfaces
{
    public interface IAdminCommandService : ICreateCommandService<CreateAdminDdto, AdminProjection>, IUpdateCommandService<UpdateAdminDdto, AdminProjection>
    {
    }
}
