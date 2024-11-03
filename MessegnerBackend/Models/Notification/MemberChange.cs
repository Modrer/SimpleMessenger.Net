using System.Text.Json.Serialization;

namespace MessegnerBackend.Models.Notification
{
    public class MemberChange
    {
        [JsonPropertyName("chat_id")]
        public int ChatId { get; set; }
        [JsonPropertyName("user_id")]
        public int UserId { get; set; }
    }
}
