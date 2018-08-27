using Newtonsoft.Json;

namespace Common.Api.Authentication.FacebookModels
{
    public class FacebookUserData
    {
        public string Name { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        public string Locale { get; set; }
        [JsonProperty("picture")]
        public FacebookPictureData PictureData { get; set; }
    }
}
