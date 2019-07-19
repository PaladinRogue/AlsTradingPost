using System.ComponentModel.DataAnnotations;
using Common.Domain.Aggregates;
using KeyVault.Domain.Applications.CreateDataKey;

namespace KeyVault.Domain.Applications
{
    public class ApplicationDataKey : DataKey<int>, IAggregateMember
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