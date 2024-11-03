using AuthentificationLibrary;

namespace MessegnerBackend
{
    public interface IUserGetter
    {
        public AuthInfo? GetUser();
        public AuthInfo? GetUser(string token);
    }
}
