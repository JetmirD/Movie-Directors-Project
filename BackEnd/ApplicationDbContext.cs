using Microsoft.EntityFrameworkCore;
using MovieDetyra.Models;
namespace MovieDetyra
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Movie> movies { get; set; }
        public DbSet<Director> directors { get; set; }
    }
}
