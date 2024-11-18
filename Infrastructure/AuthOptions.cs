using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Domain
{
    public class AuthOptions
    {
        public const string ISSUER = "AuthServer"; 
        public const string AUDIENCE = "AuthClient"; 
        const string KEY = "this_is_a_secret_key_that_is_256_bits";   
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
