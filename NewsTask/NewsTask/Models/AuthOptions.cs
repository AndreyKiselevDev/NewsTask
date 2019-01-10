using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace NewsTask.Api.Models
{
    public class AuthOptions
    {
        public const string ISSUER = "NewsTaskServer"; // издатель токена
        public const string AUDIENCE = "https://localhost:44364/"; // потребитель токена
        private const string KEY = "sadk1dj91&#h%sasdqwesad";   // ключ для шифрации
        public const int LIFETIME = 720; // время жизни токена - 1 минута

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
