using System.Reflection;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class CinemaContext : DbContext
{
    public CinemaContext(DbContextOptions<CinemaContext> options) : base(options)
    {
        this.Database.EnsureCreated();
    }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Show> Shows { get; set; }
    public DbSet<Theatre> Theatres { get; set; }
    public DbSet<Review> Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}