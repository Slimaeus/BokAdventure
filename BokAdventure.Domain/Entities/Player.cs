using BokAdventure.Domain.Common;
using BokAdventure.Domain.Helpers;

namespace BokAdventure.Domain.Entities;
public sealed class Player : BaseAuditableEntity
{
    public int Level { get; set; } = 1;
    public int Experience { get; set; } = 0;
    public int BokCoins { get; set; } = 100;
    public int RequiredExperience { get; set; } = ExperienceCalculator.CalculateRequiredExperience(1);
    public Guid? ApplicationUserId { get; set; }
    public ApplicationUser? ApplicationUser { get; set; }

    public ICollection<Bok> Boks { get; set; } = new List<Bok>();
}
