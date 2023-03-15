using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace serverapp
{
    public static class StatusDemande
    {
        public static string Accepte = "accepté";
        public static string Refusé = "refusé";
        public static string EnCours = "encours";
        public static string Acorriger = "àcorriger";
    }
    public sealed class Demande
    {
        [Key]
        public int Id { get; set; }
        public string type { get; set; }
        public DateTime Date { get; set; }
        [InverseProperty("demandes")]
        public User User { get; set; }
        [ForeignKey(name: "User")]
        public int UserId { get; set; }
        [ForeignKey(name: "Admin")]
        public int AdminId { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public ICollection<File> Files { get; set; }
    }
}