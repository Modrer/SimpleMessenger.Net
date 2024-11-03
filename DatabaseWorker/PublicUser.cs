using System.Text.Json.Serialization;

namespace DatabaseWorker
{
    public class PublicUser
    {
        [JsonPropertyName("user_id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("image")]
        public string Image { get; set; } = string.Empty;
    }
}
