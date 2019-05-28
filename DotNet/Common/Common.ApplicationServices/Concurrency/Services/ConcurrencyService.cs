﻿using System;
using Common.ApplicationServices.Concurrency.Services.Interfaces;
using Common.Domain.Exceptions;
using Common.Resources.Concurrency.Interfaces;

namespace Common.ApplicationServices.Concurrency.Services
{
    public class ConcurrencyService<T> : IConcurrencyService<T> where T : ICheckConcurrencyService
    {
        private readonly T _checkConcurrencyService;

        public ConcurrencyService(T checkConcurrencyService)
        {
            _checkConcurrencyService = checkConcurrencyService;
        }

        public void CheckConcurrency(Guid id, IConcurrencyVersion version)
        {
            if (!_checkConcurrencyService.CheckConcurrency(id, version))
            {
                throw new ConcurrencyDomainException(typeof(T), id, version);
            }
        }
    }
}