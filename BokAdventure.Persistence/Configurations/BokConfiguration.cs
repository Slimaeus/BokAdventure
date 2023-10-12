using BokAdventure.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BokAdventure.Persistence.Configurations;

public class BokConfiguration : IEntityTypeConfiguration<Bok>
{
    public void Configure(EntityTypeBuilder<Bok> builder)
    {
    }
}
