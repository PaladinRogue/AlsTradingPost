using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PaladinRogue.Library.Core.Common;
using PaladinRogue.Library.Core.Domain.Aggregates;
using PaladinRogue.Library.Core.Domain.Entities;
using PaladinRogue.Library.Vault.Domain.Applications.AddDataKey;
using PaladinRogue.Library.Vault.Domain.Applications.Create;
using PaladinRogue.Library.Vault.Domain.Applications.CreateDataKey;

namespace PaladinRogue.Library.Vault.Domain.Applications
{
    public class Application : VersionedEntity, IAggregateRoot
    {
        private readonly ISet<ApplicationDataKey> _applicationDataKeys = new HashSet<ApplicationDataKey>();

        protected Application()
        {
        }

        private Application(CreateApplicationDdto createApplicationDdto)
        {
            SystemName = createApplicationDdto.SystemName;
        }

        internal static Application Create(CreateApplicationDdto createApplicationDdto)
        {
            return new Application(createApplicationDdto);
        }

        public virtual IEnumerable<ApplicationDataKey> ApplicationDataKeys => _applicationDataKeys;

        [MaxLength(FieldSizes.Short)]
        [Required]
        public string SystemName { get; protected set; }

        internal ApplicationDataKey AddDataKey(AddApplicationDataKeyDdto addApplicationDataKeyDdto)
        {
            if (ApplicationDataKeys.SingleOrDefault(a => a.Name == addApplicationDataKeyDdto.Type) != null)
            {
                throw new ApplicationDataKeyAlreadyExistsDomainException();
            }

            ApplicationDataKey applicationDataKey = ApplicationDataKey.Create(this, new CreateDataKeyDdto
            {
                Type = addApplicationDataKeyDdto.Type,
                Value = addApplicationDataKeyDdto.Value
            });

            _applicationDataKeys.Add(applicationDataKey);

            return applicationDataKey;
        }
    }
}