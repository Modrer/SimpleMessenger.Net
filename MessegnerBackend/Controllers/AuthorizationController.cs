using AuthentificationLibrary;
using MessegnerBackend.Models.ControllersInputs;
using MessegnerBackend.Models.DataClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MessegnerBackend.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly ILogger<AuthorizationController> _logger;
        private readonly UserRegistrator _userRegistrator;
        private readonly IConfiguration _configuration;
        private readonly IUserGetter _userGetter;

        public AuthorizationController(ILogger<AuthorizationController> logger, IUserGetter userGetter, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _userGetter = userGetter;

            string? key = _configuration["JWT:Key"];
            string? issuer = _configuration["JWT:Issuer"];
            string? audience = _configuration["JWT:Audience"];

            if (string.IsNullOrEmpty(key)) {
                throw new ArgumentNullException(nameof(key));
            }

            if (string.IsNullOrEmpty(issuer))
            {
                throw new ArgumentNullException(nameof(issuer));
            }

            if (string.IsNullOrEmpty(audience))
            {
                throw new ArgumentNullException(nameof(audience));
            }

            _userRegistrator = new UserRegistrator(
                key, issuer, audience);
        }

        [HttpPatch]
        public IActionResult ChangePassword([FromBody] PasswordData password)
        {

            var user = GetUser();

            if (user == null) { 
                return Unauthorized();
            }

            var currentUserAuth = _userRegistrator.Login(user.Name, password.OldPassword);

            if (currentUserAuth != null)
            {
                if (currentUserAuth.Id != user.Id) {
                    return BadRequest();
                }

                if( !_userRegistrator.ChangePassword(currentUserAuth.Id, password.NewPassword)) { 
                    return this.StatusCode(500); 
                }

                return Ok();
            }

            return Unauthorized();
            
        }

        [HttpPut]
        public IActionResult SingUp([FromBody] Registration user)
        {

            if ( _userRegistrator.Registrate(user.Login, user.Email, user.Password))
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        [ResponseCache(Duration = 60)]
        public IActionResult Authorithe([FromBody] LoginData userLogin)
        {

            var user = _userRegistrator.Login(userLogin.Login, userLogin.Password);
            if (user != null)
            {
                var token = _userRegistrator.GenerateToken(user);

                return Ok(
                    new Authorization()
                    {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Image = user.Image,
                    Token = token
                    });
            }
            return Unauthorized();

        }
        [Authorize]
        [HttpGet]
        public IActionResult CheckAuthorization()
        {
            var userOrNull = GetUser();

            if (userOrNull == null)
            {
                return Unauthorized();
            }

            var user = userOrNull;

            return Ok(user);
        }
        private AuthInfo? GetUser()
        {

            return _userGetter.GetUser();
        }
    }
}
