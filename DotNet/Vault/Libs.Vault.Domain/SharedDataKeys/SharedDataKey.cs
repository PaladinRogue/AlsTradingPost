using PaladinRogue.Libray.Core.Domain.Aggregates;
using PaladinRogue.Libray.Vault.Domain.SharedDataKeys.Change;
using PaladinRogue.Libray.Vault.Domain.SharedDataKeys.Create;

namespace PaladinRogue.Libray.Vault.Domain.SharedDataKeys
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