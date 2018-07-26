using System.Collections.Generic;

namespace Common.Application.Validation
{
    public class ValidationError
    {
        public string ValidationErrorCode { get; set; }

        public Dictionary<string, object> ValidationMeta { get; set; }
    }
}
