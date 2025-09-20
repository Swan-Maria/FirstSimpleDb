using Microsoft.EntityFrameworkCore;

namespace FirstSimpleDb.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=demo_db;Username=demo_user;Password=demo_pass");
        }
    }
}