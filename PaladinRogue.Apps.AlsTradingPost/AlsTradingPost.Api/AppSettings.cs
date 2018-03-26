using System.Collections.Generic;

namespace AlsTradingPost.Api
{
    public class AppSettings
    {
      public string Secret { get; set; }
      public Dictionary<string, string> ConnectionStrings { get; set; }
    }
}
