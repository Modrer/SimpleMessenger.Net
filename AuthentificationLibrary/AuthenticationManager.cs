using DatabaseWorker.FlatModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthentificationLibrary
{
    public class AuthenticationManager
    {

        private readonly SigningCredentials _credentials;
        private readonly JwtSecurityTokenHandler _tokenWriter = new();

        private readonly string _algoritm = SecurityAlgorithms.HmacSha512;

        private readonly string? _issuer;
        private readonly string? _audience;

        public AuthenticationManager(string key, string? issuer, string? audience)
        {
            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key)
                );
            _credentials = new SigningCredentials(securityKey, _algoritm);

            _issuer = issuer;
            _audience = audience;

        }
        private static Claim[] Claim(AuthInfo user)
        {
            return
            [
                new Claim(ClaimTypes.Id, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Image, user.Image),
            ];
        }

        private static Claim[] Claim(int id, string email, string name, string image)
        {
            return
            [
                new Claim(ClaimTypes.Id, id.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Image, image),
            ];
        }

        public static AuthInfo? GetUserModel(IEnumerable<Claim> claims)
        {

            var Id = claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Id)?.Value;
            var Email = claims
                .FirstOrDefault(claim => 
                claim.Type == System.Security.Claims.ClaimTypes.Email || 
                claim.Type == ClaimTypes.Email)?.Value;
            var Name = claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
            var Image = claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Image)?.Value;

            if (Id != null && Email != null && Name != null && Image != null)
            {
                return new AuthInfo
                {
                    Id = int.Parse(Id),
                    Email = Email,
                    Name = Name,
                    Image = Name,
                };
            }
            return null;
        }
        public string GenerateToken(int id, string email, string name, string image, int tokenDurationDays = 15)
        {
            var claims = Claim(id, email, name, image);
            var token = new JwtSecurityToken(
                _issuer,
                _audience,
                claims,
                expires: DateTime.Now.AddDays(tokenDurationDays),
                signingCredentials: _credentials
                );
            return _tokenWriter.WriteToken(token);
        }
        public string GenerateToken(AuthInfo user, int tokenDurationDays = 15)
        {
            var claims = Claim(user);
            var token = new JwtSecurityToken(
                _issuer,
                _audience,
                claims,
                expires: DateTime.Now.AddDays(tokenDurationDays),
                signingCredentials: _credentials
                );
            return _tokenWriter.WriteToken(token);
        }

        public string GenerateToken(FlatUser user, int tokenDurationDays = 15)
        {

            return GenerateToken(
                new AuthInfo
                {
                    Id=user.Id,
                    Email=user.Email,
                    Name = user.Name,
                    Image = user.Image
                },
            tokenDurationDays);
        }


    }
}
