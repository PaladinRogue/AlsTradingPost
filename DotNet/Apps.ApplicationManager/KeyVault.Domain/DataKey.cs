using System.ComponentModel.DataAnnotations;
using Common.Domain.Entities;
using Common.Resources;
using Microsoft.IdentityModel.Tokens;

namespace KeyVault.Domain
{
    public abstract class DataKey<T> : VersionedEntity
    {
        protected DataKey()
        {
        }

        protected DataKey(T type, SymmetricSecurityKey value)
        {
            Type = type;
            Value = value;
        }

        [Required]
        [MaxLength(FieldSizes.Default)]
        public T Type { get; set; }

        [Required]
        [MaxLength(FieldSizes.Protected)]
        public SymmetricSecurityKey Value { get; set; }
    }
}