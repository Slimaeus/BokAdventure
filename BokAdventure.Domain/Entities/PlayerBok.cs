using BokAdventure.Domain.Enumerations;

namespace BokAdventure.Domain.Entities;

public sealed class PlayerBok
{
    public Guid? PlayerId { get; set; }
    public Player? Player { get; set; }
    public BokIdentify? BokId { get; set; }
    public Bok? Bok { get; set; }

    public ulong Amount { get; set; } = 0;
}
