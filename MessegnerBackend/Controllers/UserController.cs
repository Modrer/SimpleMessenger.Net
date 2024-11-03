using AuthentificationLibrary;
using DatabaseWorker;
using MessegnerBackend.Models;
using MessegnerBackend.Models.ControllersInputs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MessegnerBackend.Controllers
{
    [Route("/[controller]")]
    //[Authorize]
    [ApiController]
    public class UserController(TiedDBContext context, IUserGetter userGetter, ITokenGenerator tokenGenerator) : Controller
    {
        private readonly TiedDBContext _context = context;

        private readonly IUserGetter _userGetter = userGetter;
        private readonly ITokenGenerator _tokenGenerator = tokenGenerator;

        [HttpGet("{userId}")]
        public IActionResult GetUser(int userId)
        {
            PublicUser? user;
            try
            {
                user = _context.GetUserById(userId);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        [HttpPost("{name}")]
        public IActionResult SearchUser(string name)
        {

            IEnumerable<PublicUser>? users;
            try
            {
                users = _context.GetUsersByName(name);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(users);
        }
        [HttpPatch]
        public async Task<IActionResult> UpdateUserAsync([FromForm] UpdateUser form)
        {

            AuthInfo? user = _userGetter.GetUser();

            if (user == null)
            {
                return Unauthorized();
            }

            string fileName = user.Image;

            if (form.Image != null)
            {
                var file = await FileSaver.SaveFileAsync(form.Image);

                fileName = file.Name;
            }

            try
            {
                var updatedUser = context.UpdateUser(user.Id, form.Name, user.Email, fileName);

                var token = _tokenGenerator.GenerateToken(user.Id, user.Email, form.Name, fileName);
                
                return Ok(new Authorization
                {
                    Id = user.Id,
                    Name = form.Name,
                    Email = user.Email,
                    Image = fileName,
                    Token = token
                });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
