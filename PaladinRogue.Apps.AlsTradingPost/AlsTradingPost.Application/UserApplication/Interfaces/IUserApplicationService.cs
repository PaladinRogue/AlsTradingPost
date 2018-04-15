using System;
using AlsTradingPost.Application.UserApplication.Models;

namespace AlsTradingPost.Application.UserApplication.Interfaces
{
    public interface IUserApplicationService
    {
        UserAdto Get(Guid id);
        UserAdto FacebookUpdate(FacebookUpdateAdto admin);
    }
}
