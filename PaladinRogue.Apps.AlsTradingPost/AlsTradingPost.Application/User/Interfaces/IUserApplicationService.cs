using System;
using System.Collections.Generic;
using AlsTradingPost.Application.User.Models;

namespace AlsTradingPost.Application.User.Interfaces
{
    public interface IUserApplicationService
    {
        UserAdto Get(Guid id);
        IList<UserSummaryAdto> GetAll();
        Guid FacebookUpdate(FacebookUpdateAdto admin);
    }
}
