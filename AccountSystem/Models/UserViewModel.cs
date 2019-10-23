using Newtonsoft.Json;

namespace AccountSystem.Models.User
{
    public class GetAccessTokenViewModel
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }
        [JsonProperty(PropertyName = "token_type")]
        public string TokenType{ get; set; }
        [JsonProperty(PropertyName = "expires_in")]
        public string ExpiresIn { get; set; }
    }

    public class UserProfileViewModel
    {
        //public string 
    }
}