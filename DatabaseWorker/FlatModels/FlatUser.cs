using System.Text.Json.Serialization;

namespace DatabaseWorker.FlatModels
{
    public class FlatUser
    {
        [JsonPropertyName("user_id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
        [JsonPropertyName("password_hash")]
        public string Password { get; set; } = string.Empty;
        [JsonPropertyName("salt")]
        public string Salt { get; set; } = string.Empty;
        [JsonPropertyName("image")]
        public string Image { get; set; } = string.Empty;
    }
}
