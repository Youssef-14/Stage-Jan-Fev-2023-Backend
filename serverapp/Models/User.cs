using serverapp.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspWebApp.Data
{
    public sealed class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string Email { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string Cin { get; set; } = string.Empty;
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(50)]
        public string Type { get; set; } = string.Empty;
        [MaxLength(70)]
        public string Token { get; set; } = string.Empty;
        [Required]
        [MaxLength(70)]
        public string Password { get; set; } = string.Empty;
        [InverseProperty("User")]
        public ICollection<Demande> demandes  { get; set; }
    }
}
