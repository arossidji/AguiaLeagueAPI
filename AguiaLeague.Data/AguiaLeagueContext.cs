using AguiaLeague.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AguiaLeague.Data;

public sealed class AguiaLeagueContext : DbContext
{
    public AguiaLeagueContext(DbContextOptions<AguiaLeagueContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Time> Times { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AguiaLeagueContext).Assembly);
    }
}