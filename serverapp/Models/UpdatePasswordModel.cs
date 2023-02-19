using System.ComponentModel.DataAnnotations;

namespace serverapp
{
    public class UpdatePasswordModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(70)]
        public string OldPassword { get; set; } = string.Empty;
        [Required]
        [MaxLength(70)]
        public string NewPassword { get; set; } = string.Empty;

    }
}
