using System.Text.Json.Serialization;

namespace DatabaseWorker.FlatModels
{
    public class FlatMembership
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("chat_id")]
        public int ChatId { get; set; }
        [JsonPropertyName("user_id")]
        public int UserId { get; set; }
        [JsonPropertyName("role")]
        public string Role { get; set; } = null!;
        [JsonPropertyName("last_read_message_id")]
        public int LastReadMessageId { get; set; }
    }
}
