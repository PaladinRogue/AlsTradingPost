using System.Collections.Generic;

namespace Common.Api.Settings
{
    public class AppSettings
    {
      public string Secret { get; set; }
      public Dictionary<string, string> ConnectionStrings { get; set; }
    }
}
