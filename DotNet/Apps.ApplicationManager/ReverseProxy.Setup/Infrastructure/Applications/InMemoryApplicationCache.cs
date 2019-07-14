using System.Collections.Generic;
using System.Threading.Tasks;
using ReverseProxy.ApplicationServices.Applications;
using ReverseProxy.Domain.Applications;

namespace ReverseProxy.Setup.Infrastructure.Applications
{
    public class InMemoryApplicationCache : IApplicationCache
    {
        private readonly IDictionary<string, Application> _applications;

        public InMemoryApplicationCache()
        {
            _applications = new Dictionary<string, Application>();
        }

        public Task<Application> GetAsync(string name)
        {
            return Task.FromResult(_applications.ContainsKey(name) ? _applications[name] : null);
        }

        public Task AddAsync(Application application)
        {
            _applications.Add(application.SystemName, application);

            return Task.CompletedTask;
        }

        public Task RemoveAsync(string name)
        {
            _applications.Remove(name);

            return Task.CompletedTask;
        }

        public Task UpdateAsync(Application application)
        {
            _applications.Remove(application.SystemName);
            _applications.Add(application.SystemName, application);

            return Task.CompletedTask;
        }
    }
}