using System.Text.Json.Serialization;

namespace AuthentificationLibrary
{
    public class AuthInfo
    {
        [JsonPropertyName("user_id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
        [JsonPropertyName("image")]
        public string Image { get; set; } = string.Empty;
    }
}
