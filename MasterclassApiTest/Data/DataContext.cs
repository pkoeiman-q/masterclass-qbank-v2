using MasterclassApiTest.Entities;
using Microsoft.EntityFrameworkCore;

namespace MasterclassApiTest.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Klant> Klanten { get; set; }
        public DbSet<Medewerker> Medewerkers { get; set; }
        public DbSet<Rekening> Rekeningen { get; set; }
        public DbSet<Overboeking> Overboekingen { get; set; }

        // Make it so the username has per Gebruiker has to be unique, even though its not the primary key
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rekening>()
                .HasOne(rekening => rekening.Klant)
                .WithMany(klant => klant.Rekeningen)
                .HasForeignKey("KlantNummer");

            // Assign Adres as complex property
            modelBuilder.Entity<Klant>(x =>
            {
                x.ComplexProperty(y => y.Adres, y => { y.IsRequired(); });
            });

            // Convert role enum to string when persisting to database (and revert when getting data from database)
            modelBuilder.Entity<Klant>()
                .Property(k => k.Role)
                .HasConversion(
                    v => v.ToString(),
                    v => (UserAccessRole)Enum.Parse(typeof(UserAccessRole), v)
                );
            modelBuilder.Entity<Medewerker>()
                .Property(k => k.Role)
                .HasConversion(
                    v => v.ToString(),
                    v => (UserAccessRole)Enum.Parse(typeof(UserAccessRole), v)
                );

            modelBuilder.Entity<Klant>()
                .HasIndex(klant => klant.Id);
            modelBuilder.Entity<Medewerker>()
                .HasIndex(medewerker => medewerker.Id);
        }
    }
}
