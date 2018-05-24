﻿using System;
using Common.Domain.Models.Interfaces;

namespace Common.Domain.Services.Command
{
    public interface ICreateCommandService<in T> where T : IEntity
    {
        Guid Create(T entity);
    }
}
