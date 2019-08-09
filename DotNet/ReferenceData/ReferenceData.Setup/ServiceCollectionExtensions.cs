using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReferenceData.Application.ReferenceData;
using ReferenceData.Domain.Persistence;
using ReferenceData.Persistence;
using Common.Setup.Infrastructure.Authorisation;

namespace ReferenceData.Setup
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddReferenceData<TDbContext>(this IServiceCollection services) where TDbContext : DbContext, IReferenceDataDbContext
        {
            return services
                .AddSecureApplicationService<IReferenceDataApplicationService, ReferenceDataApplicationService, ReferenceDataApplicationServiceSecurityDecorator>()
                .AddScoped<IReferenceDataQueryRepository, ReferenceDataQueryRepository<TDbContext>>();
        }
    }
}