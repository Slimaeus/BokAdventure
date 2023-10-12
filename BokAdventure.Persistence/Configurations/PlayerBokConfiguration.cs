using BokAdventure.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BokAdventure.Persistence.Configurations;

public sealed class PlayerBokConfiguration : IEntityTypeConfiguration<PlayerBok>
{
    public void Configure(EntityTypeBuilder<PlayerBok> builder)
    {
        builder
            .HasKey(x => new { x.PlayerId, x.BokId });
    }
}
