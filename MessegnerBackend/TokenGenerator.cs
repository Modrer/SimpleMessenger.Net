using AuthentificationLibrary;

namespace MessegnerBackend
{
    public class TokenGenerator : AuthenticationManager, ITokenGenerator
    {
        public TokenGenerator(IConfiguration configuration): 
            //Can`t be null because cheked into program.cs before creating
            base(configuration["JWT:Key"], configuration["JWT:Issuer"], configuration["JWT:Audience"])
        {
        }
        public TokenGenerator(string key, string issuer, string audience): base(key, issuer, audience){ }

    }
}
