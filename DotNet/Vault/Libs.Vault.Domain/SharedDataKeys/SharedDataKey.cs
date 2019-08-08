using Common.Domain.Aggregates;
using Vault.Domain.SharedDataKeys.Change;
using Vault.Domain.SharedDataKeys.Create;

namespace Vault.Domain.SharedDataKeys
{
    public class SharedDataKey : DataKey, IAggregateRoot
    {
        protected SharedDataKey()
        {
        }

        private SharedDataKey(CreateSharedDataKeyDdto createSharedDataKeyDdto)
            : base(createSharedDataKeyDdto.Name, createSharedDataKeyDdto.Value)
        {
            Name = createSharedDataKeyDdto.Name;
        }

        internal static SharedDataKey Create(CreateSharedDataKeyDdto createSharedDataKeyDdto)
        {
            return new SharedDataKey(createSharedDataKeyDdto);
        }

        internal void Change(ChangeSharedDataKeyDdto changeSharedDataKeyDdto)
        {
            Value = changeSharedDataKeyDdto.Value;
        }
    }
}