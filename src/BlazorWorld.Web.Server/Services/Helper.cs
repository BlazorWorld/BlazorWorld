using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWorld.Web.Server.Services
{
    public static class Helper
    {
        public static string AvatarHash(string email)
        {
            string avatarHash = string.Empty;
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(email));
                avatarHash = BitConverter.ToString(hash).Replace("-", "").ToLower();
            }

            return avatarHash;
        }
    }
}
