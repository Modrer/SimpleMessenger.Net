using Microsoft.AspNetCore.Mvc;

namespace MessegnerBackend.Models.ControllersInputs
{
    public class CreateChat
    {
        [FromForm(Name = "name")]
        public string Name { get; set; } = string.Empty;
        [FromForm(Name = "image")]
        public IFormFile? Image { get; set; }
    }
}
