using System.ComponentModel.DataAnnotations;

namespace DVWA.Models.Auth
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public bool Remember { get; set; }

    }
}