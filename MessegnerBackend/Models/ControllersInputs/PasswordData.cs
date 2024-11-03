using System.Text.Json.Serialization;

namespace MessegnerBackend.Models.ControllersInputs
{
    public class PasswordData
    {
        [JsonPropertyName("old_password")]
        public string OldPassword { get; set; } = string.Empty;
        [JsonPropertyName("new_password")]
        public string NewPassword { get; set; } = string.Empty;


    }
}
