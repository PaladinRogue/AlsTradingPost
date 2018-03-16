using System;
using System.Collections.Generic;
using AlsTradingPost.Application.Admin.Models;

namespace AlsTradingPost.Application.Admin.Interfaces
{
    public interface IAdminApplicationService
    {
        AdminAdto Get(Guid id);
        IList<AdminSummaryAdto> GetAll();
        AdminAdto Create(CreateAdminAdto admin);
        AdminAdto Update(UpdateAdminAdto admin);
    }
}
