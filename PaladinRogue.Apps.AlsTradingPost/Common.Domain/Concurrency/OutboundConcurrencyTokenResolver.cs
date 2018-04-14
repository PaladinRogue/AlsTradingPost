﻿using AutoMapper;
using Common.Domain.Concurrency.Interfaces;
using Common.Domain.Models.Interfaces;
using Common.Resources.Concurrency;
using Common.Resources.Concurrency.Interfaces;

namespace Common.Domain.Concurrency
{
    public class OutboundConcurrencyTokenResolver : IValueResolver<IEntity, IVersionedProjection, IConcurrencyVersion>
    {
        private readonly IConcurrencyVersionProvider _concurrencyVersionProvider;

        public OutboundConcurrencyTokenResolver(IConcurrencyVersionProvider concurrencyVersionProvider)
        {
            _concurrencyVersionProvider = concurrencyVersionProvider;
        }

        IConcurrencyVersion IValueResolver<IEntity, IVersionedProjection, IConcurrencyVersion>.Resolve(IEntity source, IVersionedProjection destination, IConcurrencyVersion destMember, ResolutionContext context)
        {
            return _concurrencyVersionProvider.GetConcurrencyVersion(source);
        }
    }
}