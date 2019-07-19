using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Common.Domain.Aggregates;
using Common.Domain.Entities;
using Common.Resources;
using KeyVault.Domain.Applications.AddDataKey;
using KeyVault.Domain.Applications.Create;
using KeyVault.Domain.Applications.CreateDataKey;

namespace KeyVault.Domain.Applications
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
            if (ApplicationDataKeys.SingleOrDefault(a => a.Type == addApplicationDataKeyDdto.Type) != null)
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