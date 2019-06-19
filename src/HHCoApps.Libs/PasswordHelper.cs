using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HHCoApps.Libs
{
    public static class PasswordHelper
    {
        public static string Encrypt(string input)
        {
            var hash = new StringBuilder();
            var md5provider = new MD5CryptoServiceProvider();
            var bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (var i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        public static bool PasswordComplexity(string password)
        {
            var legal = false;
            legal = "0123456789".Contains(password) || "9876543210".Contains(password);
            legal = password.Length > 6;
            return legal;
        }
    }
}
