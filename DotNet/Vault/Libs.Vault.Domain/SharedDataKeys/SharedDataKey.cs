using PaladinRogue.Library.Core.Domain.Aggregates;
using PaladinRogue.Library.Vault.Domain.SharedDataKeys.Change;
using PaladinRogue.Library.Vault.Domain.SharedDataKeys.Create;

namespace PaladinRogue.Library.Vault.Domain.SharedDataKeys
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