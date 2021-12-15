using AguiaLeague.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AguiaLeague.Data.Configurations;

public class TimeConfiguration : IEntityTypeConfiguration<Time>
{
    public void Configure(EntityTypeBuilder<Time> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Nome).IsRequired();
        builder.Property(m => m.Tag).IsRequired();

        builder.ToTable("Times");
    }
}