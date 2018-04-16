using System.Collections.Generic;

namespace Common.Application.Validation
{
    public class ValidationResult
    {
        public Dictionary<string, Dictionary<string, Dictionary<string, object>>> ValidationErrors { get; set; }
    }
}
