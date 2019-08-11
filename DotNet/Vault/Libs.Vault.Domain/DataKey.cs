using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;
using PaladinRogue.Libray.Core.Common;
using PaladinRogue.Libray.Core.Domain.Entities;

namespace PaladinRogue.Libray.Vault.Domain
{
    public abstract class DataKey : VersionedEntity
    {
        protected DataKey()
        {
        }

        protected DataKey(string name, SymmetricSecurityKey value)
        {
            Name = name;
            Value = value;
        }

        [Required]
        [MaxLength(FieldSizes.Default)]
        public string Name { get; set; }

        [Required]
        [MaxLength(FieldSizes.Protected)]
        public SymmetricSecurityKey Value { get; set; }
    }
}