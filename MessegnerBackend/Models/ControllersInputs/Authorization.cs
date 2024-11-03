using System.Text.Json.Serialization;

namespace MessegnerBackend.Models.ControllersInputs
{
    public class Authorization
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
        [JsonPropertyName("image")]
        public string Image { get; set; } = string.Empty;
        [JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty;
    }
}
