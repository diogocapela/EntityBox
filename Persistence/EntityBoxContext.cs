using EntityBox.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityBox.Persistence
{
    public class EntityBoxContext : DbContext
    {
        public EntityBoxContext(DbContextOptions<EntityBoxContext> options)
            : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Person> Persons { get; set; }
    }
}