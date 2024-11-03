using System.Security.Claims;
using DatabaseWorker;
using DatabaseWorker.FlatModels;

namespace AuthentificationLibrary
{
    public class UserRegistrator
    {
        private SimpleMessengerContext _context = new();
        private readonly AuthenticationManager _authenticationManager;

        public UserRegistrator(string secretKey, string issuer, string audience)
        {
            _authenticationManager = new AuthenticationManager(
                secretKey,
                issuer,
                audience
                );
        }

        public bool ContainsUser(string userName)
        {
            return _context.ContainUser(userName);
        }
        private bool IsParametersValid(string name, string email, string password)
        {
            if (ContainsUser(name))
            {
                return false;
            }
            return true;

        }
        public bool Registrate(string name, string email, string password)
        {
            if (!IsParametersValid(name, email, password))
            {
                return false;
            }            

            PasswordHasher.HashPassword(password,
            out string passwordHash, out string salt);

            _context.AddUser(name, email, passwordHash, salt);

            return true;
        }

        public FlatUser? Login(string login, string password)
        {
            var dbUser = _context.GetUserByName(login);

            if (dbUser == null)
            {
                return null;
            }

            if (PasswordHasher.CheckPassword(password, dbUser.Salt, dbUser.Password))
            {
                return dbUser;
            }

            return null;

        }
        public string GenerateToken(AuthInfo user)
        {
            return _authenticationManager.GenerateToken(user);
        }
        public string GenerateToken(FlatUser user)
        {
            return _authenticationManager.GenerateToken(user);
        }
        public static AuthInfo? GetUser(IEnumerable<Claim> claims)
        {
            return AuthenticationManager.GetUserModel(claims);
        }

        public bool ChangePassword(int id, string newPassword)
        {
            PasswordHasher.HashPassword(newPassword, out string newPasswordHash, out string salt);

            return _context.ChangePassword(id, newPasswordHash, salt);

        }
    }
}