using KartuliAPI1.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace KartuliAPI1.Data
{
    public class KartuliRestContext : DbContext
    {
        public DbSet<Users> User { get; set; } 
        public DbSet<Recipes> Recipe { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog = KartuliApi1");
        }

    }
}
