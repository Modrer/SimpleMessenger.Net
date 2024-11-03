using Microsoft.AspNetCore.Mvc;

namespace MessegnerBackend.Models.ControllersInputs
{
    public class UpdateChat
    {
        [FromForm(Name = "id")]
        public int Id { get; set; }
        [FromForm(Name = "name")]
        public string Name { get; set; } = string.Empty;
        [FromForm(Name = "image")]
        public IFormFile? Image { get; set; }
    }
}