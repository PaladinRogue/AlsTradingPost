using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PaladinRogue.Libray.Core.Common;
using PaladinRogue.Libray.Core.Domain.Aggregates;
using PaladinRogue.Libray.Core.Domain.Entities;

namespace PaladinRogue.Libray.ReferenceData.Domain
{
    public class ReferenceDataType : VersionedEntity, IAggregateRoot
    {
        private readonly ISet<ReferenceDataValue> _referenceDataValues = new HashSet<ReferenceDataValue>();

        protected ReferenceDataType()
        {
        }

        public virtual IEnumerable<ReferenceDataValue> ReferenceDataValues => _referenceDataValues;

        [Required]
        [MaxLength(FieldSizes.Default)]
        public string Type { get; protected set; }
    }
}