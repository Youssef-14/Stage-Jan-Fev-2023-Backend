using AspWebApp.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace serverapp.Data
{
    public static class TypeDemande
    {
        public static string Accepte = "accepté";
        public static string Refusé = "refusé";
        public static string EnCours = "encours";
        public static string Acorriger = "àcorriger";

    }
    internal sealed class Demande
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Type { get; set; }
        [Required]
        [MaxLength(50)]
        public DateTime Date { get; set; }
        [InverseProperty("demandes")]
        public User User { get; set; }
        [ForeignKey(name: "User")]
        public int UserId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string Comment { get; set; } = string.Empty;
        public ICollection<File> Files { get; set; }
    }
}
