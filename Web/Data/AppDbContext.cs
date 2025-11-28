using Microsoft.EntityFrameworkCore;
using Web.Models;
using Web.Data;


namespace Web.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Colegio> Colegios { get; set; }
    }
}
