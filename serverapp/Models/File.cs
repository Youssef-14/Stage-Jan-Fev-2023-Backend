using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace serverapp
{
    public sealed class File
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string Path { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string Type { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string Size { get; set; } = string.Empty;
        [InverseProperty("files")]
        public Demande Demande { get; set; }
        [ForeignKey(name: "Demande")]
        public int DemandeId { get; set; }
        
    }
}
