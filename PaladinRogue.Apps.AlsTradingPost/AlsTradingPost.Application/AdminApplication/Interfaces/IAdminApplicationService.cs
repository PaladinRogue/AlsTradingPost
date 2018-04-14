using System;
using System.Collections.Generic;
using AlsTradingPost.Application.AdminApplication.Models;

namespace AlsTradingPost.Application.AdminApplication.Interfaces
{
    public interface IAdminApplicationService
    {
        AdminAdto Get(Guid id);
        IList<AdminSummaryAdto> GetAll();
        AdminAdto Create(CreateAdminAdto admin);
        AdminAdto Update(UpdateAdminAdto admin);
    }
}
