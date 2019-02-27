using System.ComponentModel.DataAnnotations;
using DVWA.Models.ADO;
using DVWA.Utilities;

namespace DVWA.Models.Auth
{
    public class RegisterModel
    {
        [Required]
        [MinLength(4,ErrorMessage = "{0} must be at least {1} characters")]
        public string Username { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "{0} must be at least {1} characters")]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string ConfirmPassword { get; set; }

        public User Add(DVWAEntities db)
        {
            var user = new User { username = Username, password = Password.ToMd5() };
            var rs = db.Users.Add(user);
            db.SaveChanges();
            return rs;
        }
    }
}