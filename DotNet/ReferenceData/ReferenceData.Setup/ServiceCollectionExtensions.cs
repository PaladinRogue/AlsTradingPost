using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PaladinRogue.Library.Authorisation.Setup.Infrastructure.Authorisation;
using PaladinRogue.Library.ReferenceData.Application.ReferenceData;
using PaladinRogue.Library.ReferenceData.Domain.Persistence;
using PaladinRogue.Library.ReferenceData.Persistence;

namespace PaladinRogue.Library.ReferenceData.Setup
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