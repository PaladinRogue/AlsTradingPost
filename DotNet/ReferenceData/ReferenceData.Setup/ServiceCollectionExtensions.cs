using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PaladinRogue.Libray.Authorisation.Setup.Infrastructure.Authorisation;
using PaladinRogue.Libray.ReferenceData.Application.ReferenceData;
using PaladinRogue.Libray.ReferenceData.Domain.Persistence;
using PaladinRogue.Libray.ReferenceData.Persistence;

namespace PaladinRogue.Libray.ReferenceData.Setup
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