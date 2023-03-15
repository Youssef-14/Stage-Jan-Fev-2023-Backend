using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace serverapp
{
    public sealed class File
    {
        [Key]
        public int Id { get; set; }
        public string Path { get; set; } = string.Empty;
        [InverseProperty("files")]
        public Demande Demande { get; set; }
        [ForeignKey(name: "Demande")]
        public int DemandeId { get; set; }
    }
}
