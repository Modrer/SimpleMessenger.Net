using Microsoft.AspNetCore.Mvc;

namespace MessegnerBackend.Models.ControllersInputs
{
    public class UpdateUser
    {
        //[FromForm(Name = "email")]
        //public string Email { get; set; } = string.Empty;
        [FromForm(Name = "name")]
        public string Name { get; set; } = string.Empty;
        [FromForm(Name = "image")]
        public IFormFile? Image { get; set; }
    }
}