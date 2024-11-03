using System.Text.Json.Serialization;

namespace MessegnerBackend.Models.Notification
{
    public class Notification
    {
        [JsonPropertyName("message_type")]
        public string Type { get; set; } = string.Empty;
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
    }
}
