using Microsoft.EntityFrameworkCore;

namespace serverapp
{
    public class DBContextMocked : DbContext
    {
        public DBContextMocked(DbContextOptions<DBContextMocked> options)
            : base(options)
        {
        }
        
        public DbSet<Demande> Demandes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<File> Files { get; set; }

        [Obsolete]
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Demande>().HasCheckConstraint("CK_Demande_Status", "Status IN ( 'accepté', 'encours', 'refusé','àcorriger')");
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