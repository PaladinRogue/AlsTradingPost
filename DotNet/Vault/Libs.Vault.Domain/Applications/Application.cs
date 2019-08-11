using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PaladinRogue.Libray.Core.Common;
using PaladinRogue.Libray.Core.Domain.Aggregates;
using PaladinRogue.Libray.Core.Domain.Entities;
using PaladinRogue.Libray.Vault.Domain.Applications.AddDataKey;
using PaladinRogue.Libray.Vault.Domain.Applications.Create;
using PaladinRogue.Libray.Vault.Domain.Applications.CreateDataKey;

namespace PaladinRogue.Libray.Vault.Domain.Applications
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