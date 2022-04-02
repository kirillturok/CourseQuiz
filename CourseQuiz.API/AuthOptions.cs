using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AuthTutorial.Auth.Common;

public static class AuthOptions
{
    public const string ISSUER = "MyAuthServer"; // издатель токена
    public const string AUDIENCE = "MyAuthClient"; // потребитель токена
    const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
    public const int LIFETIME = 1; // время жизни токена - 1 day
    public static SymmetricSecurityKey GetSymmetricSecurityKey =
        new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
}