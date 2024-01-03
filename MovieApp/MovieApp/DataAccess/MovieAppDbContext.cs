using Microsoft.EntityFrameworkCore;
using MovieApp.DataAccess.Data.Entities;

namespace MovieApp.DataAccess;

public class MovieAppDbContext : DbContext
{
    public MovieAppDbContext(DbContextOptions<MovieAppDbContext> options) : base(options)
    {
    }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<Artist> Artists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=MovieAppStorage;Integrated Security=True;Encrypt=False");
    }
}
