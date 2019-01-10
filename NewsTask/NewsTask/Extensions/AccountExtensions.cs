using System;
using System.Security.Cryptography;
using System.Text;

namespace NewsTask.Api.Extensions
{
    public static class AccountExtensions
    {
        public static string EncodePassword(this string password)
        {
            var bytes = Encoding.Unicode.GetBytes(password);
            var inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
            return Convert.ToBase64String(inArray);
        }
    }
}
