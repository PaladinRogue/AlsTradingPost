using System.ComponentModel.DataAnnotations;
using ApplicationManager.Domain.Applications.Events;
using Common.Domain.DomainEvents;
using Common.Domain.Models;
using Common.Domain.Models.Interfaces;

namespace ApplicationManager.Domain.Applications
{
    public class Application : VersionedEntity, IAggregateRoot
    {
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
            Application application = new Application(createApplicationDdto);

            DomainEvents.Raise(ApplicationCreatedDomainEvent.Create(application));

            return application;
        }

        [MaxLength(20)]
        public string Name { get; protected set; }

        [MaxLength(20)]
        public string SystemName { get; protected set; }
        
        public void Change(ChangeApplictionDdto changeApplictionDdto)
        {
            Name = changeApplictionDdto.Name;
            SystemName = changeApplictionDdto.SystemName;
        }
    }
}
