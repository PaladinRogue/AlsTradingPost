using System;
using Common.Domain.Models.Interfaces;

namespace ApplicationManager.Domain.Applications
{
    public class ApplicationAuthenticationService : IAggregateMember
    {
        public Guid ApplicationId { get; protected set; }

        public Guid AuthenticationServiceId { get; protected set; }

        public Application Application { get; protected set; }
        
        public IAggregateRoot AggregateRoot => Application;
    }
}
