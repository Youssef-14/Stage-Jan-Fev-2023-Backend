using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
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
            optionsBuilder.UseSqlite ("Data Source=./Data/AppDB.db");
        }
        [Obsolete]
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Demande>().HasCheckConstraint("CK_Demande_Type", "Type IN ( 'accepté', 'encours', 'refusé','àcorriger')");
            modelBuilder.Entity<User>().HasCheckConstraint("CK_User_Type", "Type IN ('admin', 'user')");
            modelBuilder.Entity<User>().HasIndex(m => m.Cin)
                                     .HasName("Index1")
                                        .IsUnique();
            modelBuilder.Entity<User>().HasIndex(m => m.Email)
                                    .HasName("Index2")
                                       .IsUnique();
        }
    }
}
