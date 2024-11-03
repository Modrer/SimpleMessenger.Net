using System.Text.Json.Serialization;

namespace DatabaseWorker
{
    public partial class PersonalChat
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;
        [JsonPropertyName("owner_id")]
        public int OwnerId { get; set; }
        [JsonPropertyName("image")]
        public string Image { get; set; } = null!;
        [JsonPropertyName("last_read_message_id")]
        public int LastReadMessageId { get; set; }

    }

}
