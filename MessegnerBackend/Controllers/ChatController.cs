using AuthentificationLibrary;
using Microsoft.AspNetCore.Mvc;
using MessegnerBackend.Models;
using MessegnerBackend.Models.ControllersInputs;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace MessegnerBackend.Controllers
{
    [Route("/[controller]")]
    //[Authorize]
    [ApiController]
    public class ChatController(TiedDBContext context, IUserGetter userGetter) : Controller
    {

        private readonly TiedDBContext _context = context;

        private readonly IUserGetter _userGetter = userGetter;

        [HttpPut]
        public async Task<IActionResult> CreateChatAsync([FromForm] CreateChat chatForm)
        {

            AuthInfo? user = _userGetter.GetUser();

            if (user == null)
            {
                return Unauthorized();
            }

            string? fileName = null;

            if (chatForm.Image != null)
            {
                var file = await FileSaver.SaveFileAsync(chatForm.Image);

                fileName = file.Name;
            }

            try
            {
                var chat = fileName switch
                {
                    null => _context.CreateChat(user.Id, chatForm.Name),
                    _ => _context.CreateChat(user.Id, chatForm.Name, fileName)
                };
                return Ok(chat);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            

            
        }
        [HttpPatch]
        public async Task<IActionResult> UpdateChatAsync([FromForm] UpdateChat chatForm)
        {

            AuthInfo? user = _userGetter.GetUser();

            if (user == null)
            {
                return Unauthorized();
            }

            string? fileName = null;

            if (chatForm.Image != null)
            {
                var file = await FileSaver.SaveFileAsync(chatForm.Image);

                fileName = file.Name;
            }

            try
            {
                var chat = context.UpdateChat(chatForm.Id, chatForm.Name, fileName);

                return Ok(chat);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }



        }
        [HttpDelete]
        public IActionResult RemoveChat([FromBody] IdInput chatId)
        {
            AuthInfo? user = _userGetter.GetUser();

            if (user == null)
            {
                return Unauthorized();
            }
            _context.DeleteChat(chatId.Id, user.Id);

            return Ok();
        }
        [HttpGet]
        public IActionResult GetChats()
        {
            AuthInfo? user = _userGetter.GetUser();

            if (user == null)
            {
                return Unauthorized();
            }

            var chats = _context.GetAllChats(user.Id);

            return Ok(chats);
        }
    }
}
