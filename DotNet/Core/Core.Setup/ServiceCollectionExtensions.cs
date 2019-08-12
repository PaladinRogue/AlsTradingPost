using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NodaTime;
using PaladinRogue.Library.Core.Application.Services.Query;
using PaladinRogue.Library.Core.Application.Transactions;
using PaladinRogue.Library.Core.Setup.Infrastructure.Concurrency;
using PaladinRogue.Library.Core.Setup.Infrastructure.Routing;

namespace PaladinRogue.Library.Core.Setup
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseTransientTransactions(this IServiceCollection services)
        {
            return services
                .AddSingleton<ITransactionManager, TransientTransactionManager>();
        }

        public static IServiceCollection AddDefaultMvcOptions(this IServiceCollection services)
        {
            services.AddMvc(options =>
                {
                    options.Conventions.Add(new ApiExplorerVisibilityEnabledConvention());
                })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            return services;
        }

        public static IServiceCollection RegisterCommonApplicationServices(this IServiceCollection services)
        {
            return services
                .AddScoped(typeof(IQueryService<>), typeof(QueryService<>));
        }

        public static IServiceCollection AddCommonProviders(this IServiceCollection services)
        {
            return services
                .AddSingleton<IConcurrencyVersionProvider, ConcurrencyVersionProvider>()
                .AddSingleton<IApiDescriptionGroupCollectionProvider, ApiDescriptionGroupCollectionProvider>()
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public static IServiceCollection UseSystemClock(this IServiceCollection services)
        {
            return services
                .AddSingleton<IClock>(SystemClock.Instance);
        }

        public static IServiceCollection UseFluentValidation(this IServiceCollection services)
        {
            ValidatorOptions.LanguageManager.Enabled = false;

            return services;
        }
    }
}