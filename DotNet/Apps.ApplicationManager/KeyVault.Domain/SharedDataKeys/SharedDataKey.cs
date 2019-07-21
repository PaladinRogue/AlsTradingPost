using Common.Domain.Aggregates;
using KeyVault.Domain.SharedDataKeys.Change;
using KeyVault.Domain.SharedDataKeys.Create;

namespace KeyVault.Domain.SharedDataKeys
{
    public class SharedDataKey : DataKey<SharedDataKeyType>, IAggregateRoot
    {
        protected SharedDataKey()
        {
        }

        private SharedDataKey(CreateSharedDataKeyDdto createSharedDataKeyDdto)
            : base(createSharedDataKeyDdto.Type, createSharedDataKeyDdto.Value)
        {
            Type = createSharedDataKeyDdto.Type;
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