using System;
using System.Collections.Generic;
using AlsTradingPost.Application.Admin.Models;

namespace AlsTradingPost.Application.Admin.Interfaces
{
    public interface IAdminApplicationService
    {
        IList<AdminAdto> GetAdmins();
        AdminAdto GetAdminById(Guid id);
        AdminAdto Create(CreateAdminAdto admin);
    }
}
