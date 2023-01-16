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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasCheckConstraint("CK_User_Type", "Type IN ('admin', 'user')");
            
        }
    }
}
