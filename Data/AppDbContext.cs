using API_Code_First_001.Entities;
using Microsoft.EntityFrameworkCore;

namespace API_Code_First_001.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Person> Persons { get; set; }

    }
}
