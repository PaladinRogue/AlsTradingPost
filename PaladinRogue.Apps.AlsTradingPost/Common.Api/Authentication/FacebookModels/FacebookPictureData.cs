using Newtonsoft.Json;

namespace Common.Api.Authentication.FacebookModels
{
    public class FacebookPictureData
    {
        [JsonProperty("data")]
        public FacebookPicture Picture { get; set; }
    }
}