﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace serverapp
{
    public sealed class User
    {
        [Key]
        public int Id { get; set; }
        [Index(IsUnique = true)]
        public string Email { get; set; } = string.Empty;
        [Index(IsUnique = true)]
        public string Cin { get; set; } = string.Empty;
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(50)]
        public string Type { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        [InverseProperty("User")]
        public ICollection<Demande> demandes  { get; set; }
    }
}
