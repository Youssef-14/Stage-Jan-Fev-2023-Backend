using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace serverapp.Data
{
    public enum Type
    {
        Accepté,
        Refusé,
        Annulé,
        EnAttente,
        EnCours,
        EnAttenteDeValidation,
    }
    internal sealed class Demande
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public Type Type { get; set; }
        [Required]
        [MaxLength(50)]
        public DateTime Date { get; set; }
        [Required]
        [MaxLength(50)]
        public string User { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string Comment { get; set; } = string.Empty;
        [Required]
        public File[] Files { get; set; }
    }
}
