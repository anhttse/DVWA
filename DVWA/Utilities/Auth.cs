using System.Linq;
using System.Security.Claims;
using System.Text;
using DVWA.Models.ADO;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace DVWA.Utilities
{
    public static class Auth
    {
        public static string ToMd5(this string src)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                var inputBytes = System.Text.Encoding.ASCII.GetBytes(src);
                var hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                var sb = new StringBuilder();
                foreach (var b in hashBytes)
                {
                    sb.Append(b.ToString("X2"));
                }
                return sb.ToString();
            }

        }

        public static void SignIn(User user, IOwinContext context)
        {
            var claims = new[] {
                new Claim(ClaimTypes.Name, user.username),
                new Claim(ClaimTypes.NameIdentifier,user.user_id.ToString())
            };

            var identity = new ClaimsIdentity(claims, "ApplicationCookie");
            var authManager = context.Authentication;

            authManager.SignIn(new AuthenticationProperties
            { IsPersistent = true }, identity);
        }

        public static void Initial()
        {
            using (var db = new DVWAEntities())
            {
                var hasAdmin = db.Users.Count(x => x.username == "admin");
                if (hasAdmin != 0) return;
                db.Users.Add(new User { username = "admin", password = "password".ToMd5(), name = "Administrator" });
                db.SaveChanges();
            }
        }
    }
}