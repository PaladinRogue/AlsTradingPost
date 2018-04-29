using System;
using AlsTradingPost.Application.Admin.Models;

namespace AlsTradingPost.Application.Admin.Interfaces
{
    public interface IAdminApplicationService
    {
        AdminAdto Get(Guid id);
        AdminAdto Create(CreateAdminAdto admin);
        AdminAdto Update(UpdateAdminAdto admin);
    }
}
