using System.Text.Json.Serialization;

namespace MessegnerBackend.Models.ControllersInputs
{
    public class IdInput
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}
