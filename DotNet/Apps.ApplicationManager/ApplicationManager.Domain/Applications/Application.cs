using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Domain.Models;

namespace ApplicationManager.Domain.Applications
{
    public class Application : AggregateRoot
    {
        private readonly ISet<ApplicationAuthenticationService> _applicationAuthenticationServices = new HashSet<ApplicationAuthenticationService>();

        protected Application()
        {
        }
        protected Application(CreateApplicationDdto createApplicationDdto)
        {
            Name = createApplicationDdto.Name;
            SystemName = createApplicationDdto.SystemName;
        }

        public static Application Create(CreateApplicationDdto createApplicationDdto) 
        {
            return new Application(createApplicationDdto);
        }

        [MaxLength(20)]
        public string Name { get; protected set; }

        [MaxLength(20)]
        public string SystemName { get; protected set; }

        public IEnumerable<ApplicationAuthenticationService> ApplicationAuthenticationServices => _applicationAuthenticationServices;

        public void Change(ChangeApplictionDdto changeApplictionDdto)
        {
            Name = changeApplictionDdto.Name;
            SystemName = changeApplictionDdto.SystemName;
        }
    }
}
