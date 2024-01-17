using MasterclassApiTest.Entities;
using Microsoft.EntityFrameworkCore;

namespace MasterclassApiTest.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Gebruiker> Gebruikers { get; set; }
        public DbSet<Klant> Klanten { get; set; }

        // Make it so the username has per Gebruiker has to be unique, even though its not the primary key
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gebruiker>()
                .HasIndex(g => new { g.GebruikersNaam })
                .IsUnique(true);
        }
    }
}
