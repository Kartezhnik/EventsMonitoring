using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Common
{
    public class AuthOptions
    {
        public const string ISSUER = "AuthServer"; 
        public const string AUDIENCE = "AuthClient"; 
        const string KEY = "key12345678901234567890key";   
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
