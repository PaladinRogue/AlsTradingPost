using System;
using System.ComponentModel.DataAnnotations;
using PaladinRogue.Gateway.Domain.Applications.Change;
using PaladinRogue.Gateway.Domain.Applications.Create;
using PaladinRogue.Library.Core.Common;
using PaladinRogue.Library.Core.Domain.Aggregates;
using PaladinRogue.Library.Core.Domain.Entities;

namespace PaladinRogue.Gateway.Domain.Applications
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
            HostUri = new Uri(createApplicationDdto.HostUri);
        }

        internal static Application Create(CreateApplicationDdto createApplicationDdto)
        {
            return new Application(createApplicationDdto);
        }

        [MaxLength(FieldSizes.Default)]
        [Required]
        public string Name { get; protected set; }

        [MaxLength(FieldSizes.Short)]
        [Required]
        public string SystemName { get; protected set; }

        [MaxLength(FieldSizes.Extended)]
        [Required]
        public Uri HostUri { get; protected set; }

        internal void Change(ChangeApplicationDdto changeApplicationDdto)
        {
            Name = changeApplicationDdto.Name;
            HostUri = new Uri(changeApplicationDdto.HostUri);
        }
    }
}
