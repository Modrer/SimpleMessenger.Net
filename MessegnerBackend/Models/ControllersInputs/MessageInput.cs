using System.Text.Json.Serialization;

namespace MessegnerBackend.Models.ControllersInputs
{
    public class MessageInput
    {
        [JsonPropertyName("chat_id")]
        public int ChatId { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
    }
}
