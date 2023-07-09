using Microsoft.EntityFrameworkCore;
using UsefulPlantsCatalog.Model;

namespace Pharmacy.Model
{
    internal class BDContext : DbContext
    {
        public DbSet<Plant> CatalogOfPlants { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-MV43C0T;Database=UsefulPlants;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
