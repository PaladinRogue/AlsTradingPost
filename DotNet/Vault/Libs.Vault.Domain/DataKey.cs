using System.ComponentModel.DataAnnotations;
using Common.Domain.Entities;
using Common.Resources;
using Microsoft.IdentityModel.Tokens;

namespace Vault.Domain
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