using System.Security.Cryptography;
using System.Text;

namespace AuthentificationLibrary
{
    public static class PasswordHasher
    {
        const int s_saltLenght = 16;
        static readonly SHA256 s_sHA256 = SHA256.Create();
        static readonly RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();

        public static string Hash(string password, string salt)
        {
            string saltedPassword = $"{password}:{salt}";

            return string.Concat(
                s_sHA256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword))
                .Select(SHA256Byte => SHA256Byte.ToString("x2")));
        }

        public static bool CheckPassword(string password, string salt, string hash)
        {
            string passwordHash = Hash(password, salt);
            return passwordHash == hash;
        }
        public static void HashPassword(string password, out string passwordHash, out string salt)
        {
            salt = GetRandomlyGenerateSalt();
            passwordHash = Hash(password, salt);
        }
     
        private static string GetRandomlyGenerateSalt()
        {
            byte[] salt = new byte[s_saltLenght];
            randomNumberGenerator.GetBytes(salt);
            return Convert.
            ToBase64String(salt).TrimEnd('=');
        }
    }
}