using System;
using System.Collections.Generic;
using AlsTradingPost.Application.UserApplication.Models;

namespace AlsTradingPost.Application.UserApplication.Interfaces
{
    public interface IUserApplicationService
    {
        UserAdto Get(Guid id);
        IList<UserSummaryAdto> GetAll();
        UserAdto FacebookUpdate(FacebookUpdateAdto admin);
    }
}
