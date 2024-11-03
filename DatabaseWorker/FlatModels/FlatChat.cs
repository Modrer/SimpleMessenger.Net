using System.Text.Json.Serialization;

namespace DatabaseWorker.FlatModels
{
    public class FlatChat
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;
        [JsonPropertyName("owner_id")]
        public int OwnerId { get; set; }
        [JsonPropertyName("image")]
        public string Image { get; set; } = null!;
    }
}
