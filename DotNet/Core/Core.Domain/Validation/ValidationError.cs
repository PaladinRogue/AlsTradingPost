using System.Collections.Generic;

namespace PaladinRogue.Library.Core.Domain.Validation
{
    public class ValidationError
    {
        public string ValidationErrorCode { get; set; }

        public Dictionary<string, object> ValidationMeta { get; set; }
    }
}
