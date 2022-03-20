using AguiaLeague.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AguiaLeague.Data;

public sealed class AguiaLeagueContext : DbContext
{
    public AguiaLeagueContext(DbContextOptions<AguiaLeagueContext> options) : base(options) => Database.EnsureCreated();
    protected override void OnModelCreating(ModelBuilder modelBuilder) 
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(AguiaLeagueContext).Assembly);

    public DbSet<Time> Times { get; set; } = null!;
}