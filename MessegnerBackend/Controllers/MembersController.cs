using MessegnerBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MessegnerBackend.Controllers
{
    [Route("/[controller]")]
    //[Authorize]
    [ApiController]
    public class MembersController(TiedDBContext context, IUserGetter userGetter) : Controller
    {
        private readonly TiedDBContext _context = context;

        private readonly IUserGetter _userGetter = userGetter;

        [HttpPost("{chatId}/{messageId}")]
        public IActionResult SetMessages(int chatId, int messageId)
        {
            var user = _userGetter.GetUser();
            if (user == null)
            {
                return Unauthorized();
            }

            try
            {
                _context.SetLastReadMessage(chatId, user.Id, messageId);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPut("{chatId}/{memberId}")]
        public IActionResult AddMember(int chatId, int memberId)
        {

            //var user = userGetter.GetUser(HttpContext.User.Identity as ClaimsIdentity);

            try
            {
                _context.AddMember(chatId, memberId);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("{chatId}")]
        public IActionResult GetMembers(int chatId)
        {

            //var user = userGetter.GetUser(HttpContext.User.Identity as ClaimsIdentity);

            try
            {
                var members = _context.GetMembers(chatId);
                return Ok(members);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete("{chatId}/{memberId}")]
        public IActionResult RemoveMember(int chatId, int memberId)
        {
            //var user = userGetter.GetUser(HttpContext.User.Identity as ClaimsIdentity);

            try
            {
                _context.RemoveMember(chatId, memberId);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
