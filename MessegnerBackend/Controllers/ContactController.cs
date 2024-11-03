using MessegnerBackend.Models;
using MessegnerBackend.Models.ControllersInputs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MessegnerBackend.Controllers
{
    [Route("/[controller]")]
    //[Authorize]
    [ApiController]
    public class ContactController(TiedDBContext context, IUserGetter userGetter) : Controller
    {
        private readonly TiedDBContext _context = context;

        private readonly IUserGetter _userGetter = userGetter;

        [HttpPut]
        public IActionResult AddContact([FromBody] IdInput idInput)
        {

            var user = _userGetter.GetUser();

            if (user == null)
            {
                return Unauthorized();
            }
            try
            {
                var contact = _context.AddContact(user.Id, idInput.Id);
                return Ok(contact);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
           
        }
        [HttpDelete]
        public IActionResult RemoveContact([FromBody] IdInput idInput)
        {

            var user = _userGetter.GetUser();

            if (user == null)
            {
                return Unauthorized();
            }
            try
            {
                context.RemoveContact(user.Id, idInput.Id);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        public IActionResult GetContacts()
        {
            var user = _userGetter.GetUser();

            if (user == null)
            {
                return Unauthorized();
            }
            try
            {
                var contacts = context.GetContacts(user.Id);
                return Ok(contacts);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
