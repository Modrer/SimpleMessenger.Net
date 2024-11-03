using System.Text.Json.Serialization;

namespace MessegnerBackend.Models.DataClasses
{
    public class LoginData
    {
        [JsonPropertyName("login")]
        public string Login { get; set; } = string.Empty;
        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;

    }
}
