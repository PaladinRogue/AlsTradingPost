using System.ComponentModel.DataAnnotations;
using PaladinRogue.Libray.Core.Domain.Aggregates;
using PaladinRogue.Libray.Vault.Domain.Applications.CreateDataKey;

namespace PaladinRogue.Libray.Vault.Domain.Applications
{
    public class ApplicationDataKey : DataKey, IAggregateMember
    {
        protected ApplicationDataKey()
        {
        }

        private ApplicationDataKey(Application application, CreateDataKeyDdto createDataKeyDdto)
            : base(createDataKeyDdto.Type, createDataKeyDdto.Value)
        {
            Application = application;
        }

        internal static ApplicationDataKey Create(Application application, CreateDataKeyDdto createDataKeyDdto)
        {
            return new ApplicationDataKey(application, createDataKeyDdto);
        }

        [Required]
        public virtual Application Application { get; set; }

        public IAggregateRoot AggregateRoot => Application;
    }
}