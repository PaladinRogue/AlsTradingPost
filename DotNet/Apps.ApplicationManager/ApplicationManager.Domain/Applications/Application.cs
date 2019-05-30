using System.ComponentModel.DataAnnotations;
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

        internal static Application Create(CreateApplicationDdto createApplicationDdto) 
        {
            return new Application(createApplicationDdto);
        }

        [MaxLength(40)]
        [Required]
        public string Name { get; protected set; }

        [MaxLength(20)]
        [Required]
        public string SystemName { get; protected set; }
        
        internal void Change(ChangeApplicationDdto changeApplicationDdto)
        {
            Name = changeApplicationDdto.Name;
        }
    }
}
