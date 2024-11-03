using System.Text.Json.Serialization;

namespace DatabaseWorker.FlatModels
{
    public class FlatContact
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("owner_id")]
        public int OwnerId { get; set; }
        [JsonPropertyName("contact_id")]
        public int ContactId { get; set; }
    }
}
