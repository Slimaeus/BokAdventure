using BokAdventure.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BokAdventure.Persistence.Configurations;
public class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder
            .HasOne(p => p.ApplicationUser)
            .WithOne(au => au.Player)
            .HasForeignKey<ApplicationUser>(au => au.PlayerId);
    }
}
