﻿using serverapp.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspWebApp.Data
{
    public enum TypeUser
    {
        Admin,
        User
    }
    internal sealed class User
    {
        [Key]
        public int Id { get; set; }
        [Index(IsUnique = true)]
        [Required]
        [MaxLength(50)]
        public string Email { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string Cin { get; set; } = string.Empty;
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(50)]
        public TypeUser Type { get; set; }
        [Required]
        [MaxLength(50)]
        public string Password { get; set; } = string.Empty;
        [InverseProperty("User")]
        public ICollection<Demande> demandes  { get; set; }
    }
}
