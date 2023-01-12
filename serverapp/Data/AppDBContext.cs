using Microsoft.EntityFrameworkCore;
using serverapp.Data;

namespace AspWebApp.Data
{
    internal sealed class AppDBContext : DbContext
    {
        public DbSet<Demande> Demandes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<serverapp.Data.File> Files { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=./Data/AppDB.db");
        }
        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Demande>().ToTable("Demandes").HasKey(e => e.Id).HasName("PK_Employes");
            modelBuilder.Entity<Demande>().ToTable("Demande").Property(e => e.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Demande>().ToTable("Demandes").Property(e => e.Email).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Demande>().ToTable("Demandes").Property(e => e.Department).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<User>().ToTable("Users").HasKey(u => u.Id).HasName("PK_Users");
            modelBuilder.Entity<User>().ToTable("Users").Property(u => u.Email).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<User>().ToTable("Users").Property(u => u.Name).HasMaxLength(50);
            modelBuilder.Entity<User>().ToTable("Users").Property(u => u.Type).HasMaxLength(50);
            modelBuilder.Entity<User>().ToTable("Users").Property(u => u.Password).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<User>().ToTable("Users").HasIndex(u => u.Email).IsUnique();
        }*/
    }
}
