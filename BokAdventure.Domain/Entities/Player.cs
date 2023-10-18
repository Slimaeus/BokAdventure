using BokAdventure.Domain.Common;
using BokAdventure.Domain.Helpers;

namespace BokAdventure.Domain.Entities;
public sealed class Player : BaseAuditableEntity
{
    public ulong Level { get; set; } = 1;
    public ulong Experience { get; set; } = 0;
    public long BokCoins { get; set; } = 100;
    public long BokBank { get; set; } = 0;
    public ulong RequiredExperience { get; set; } = ExperienceCalculator.CalculateRequiredExperience(1);
    public ulong HitPoints { get; set; } = 100;
    public long Defence { get; set; } = 1;
    public long Attack { get; set; } = 1;
    public long StatPoints { get; set; } = 0;

    public Guid? ApplicationUserId { get; set; }
    public ApplicationUser? ApplicationUser { get; set; }

    public ICollection<PlayerBok> PlayerBoks { get; set; } = new List<PlayerBok>();
}
