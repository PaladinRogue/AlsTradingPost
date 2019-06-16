using System;
using System.Reflection;
using Common.ApplicationServices.Services.Query;
using Common.Domain.Models.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Authorisation
{
    public class ResourceOwnerProvider : IResourceOwnerProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public ResourceOwnerProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IAggregateOwner GetOwner(Type resourceType, Guid resourceId)
        {
            Type queryServiceType = typeof(IQueryService<>).MakeGenericType(resourceType);

            object queryService = _serviceProvider.GetRequiredService(queryServiceType);

            MethodInfo getByIdMethod = queryService.GetType().GetMethod("GetById");

            if (getByIdMethod == null)
            {
                throw new ArgumentException("The query service does not have a method GetById");
            }

            object result = getByIdMethod.Invoke(queryService, new object[]
            {
                resourceId
            });

            if (result is IOwnedAggregate ownedAggregate)
            {
                return ownedAggregate.GetOwner();
            }

            throw new ArgumentException("The resource type specified is not an owned aggregate");
        }
    }
}
