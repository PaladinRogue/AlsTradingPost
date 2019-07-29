using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReferenceData.Domain.Persistence;
using ReferenceData.Persistence;

namespace ReferenceData.Setup
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddReferenceData<TDbContext>(this IServiceCollection services) where TDbContext : DbContext, IReferenceDataDbContext
        {
            return services
                .AddScoped<IReferenceDataQueryRepository, ReferenceDataQueryRepository<TDbContext>>();
        }
    }
}