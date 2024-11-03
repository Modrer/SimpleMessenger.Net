using System.Text.Json.Serialization;

namespace MessegnerBackend.Models.ControllersInputs
{
    public class Registration
    {
        [JsonPropertyName("name")]
        public string Login { get; set; } = string.Empty;
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;

    }
}
