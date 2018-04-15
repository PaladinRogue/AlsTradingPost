using System;
using AlsTradingPost.Application.AdminApplication.Models;

namespace AlsTradingPost.Application.AdminApplication.Interfaces
{
    public interface IAdminApplicationService
    {
        AdminAdto Get(Guid id);
        AdminAdto Create(CreateAdminAdto admin);
        AdminAdto Update(UpdateAdminAdto admin);
    }
}
