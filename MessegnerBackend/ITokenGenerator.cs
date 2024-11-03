using AuthentificationLibrary;
using DatabaseWorker.FlatModels;

namespace MessegnerBackend
{
    public interface ITokenGenerator
    {
        public string GenerateToken(AuthInfo user, int tokenDurationDays = 15);
        public string GenerateToken(FlatUser user, int tokenDurationDays = 15);
        public string GenerateToken(int id, string email, string name, string image, int tokenDurationDays = 15);
    }
}
