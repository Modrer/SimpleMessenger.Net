using AuthentificationLibrary;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MessegnerBackend
{
    public class UserGetter(IHttpContextAccessor httpContextAccessor) : IUserGetter
    {

        private readonly JwtSecurityTokenHandler _tokenHandler = new();
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public AuthInfo? GetUser()
        {
            var claims = _httpContextAccessor.HttpContext?.User.Claims;

            if (claims == null)
            {
                return null;
            }
            return UserRegistrator.GetUser(claims);
        }
        public AuthInfo? GetUser(string token)
        {
            var readed = _tokenHandler.ReadJwtToken(token);

            return UserRegistrator.GetUser(readed.Claims);
        }
    }
}
