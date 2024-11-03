using MessegnerBackend.Models;
using MessegnerBackend.Models.ControllersInputs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MessegnerBackend.Controllers
{
    [Route("/[controller]")]
    //[Authorize]
    [ApiController]
    public class MessagesController(TiedDBContext context, IUserGetter userGetter) : Controller
    {
        private readonly TiedDBContext _context = context;

        private readonly IUserGetter _userGetter = userGetter;

        [HttpPost]
        public IActionResult SendMessage([FromBody] MessageInput input)
        {

            var user = _userGetter.GetUser();

            if (user == null)
            {
                return Unauthorized();
            }

            try
            {
                _context.SendMessage(user.Id, input.ChatId, input.Message);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("{chatId}")]
        public IActionResult GetMessages(int chatId)
        {
            //var user = UserGetter.GetUser(HttpContext.User.Identity as ClaimsIdentity);
            try
            {
                var messages = _context.GetMessages(chatId);
                return Ok(messages);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
