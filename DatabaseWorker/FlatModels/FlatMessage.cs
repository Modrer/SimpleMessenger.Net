using System.Text.Json.Serialization;

namespace DatabaseWorker.FlatModels
{
    public class FlatMessage
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("chat_id")]
        public int ChatId { get; set; }
        [JsonPropertyName("sender_id")]
        public int SenderId { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; } = null!;
    }
}
