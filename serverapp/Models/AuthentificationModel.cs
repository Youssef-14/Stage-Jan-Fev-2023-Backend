using System.ComponentModel.DataAnnotations;

namespace serverapp
{
    public class AuthentificationModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
