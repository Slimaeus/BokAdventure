using BokAdventure.Domain.Common;
using BokAdventure.Domain.Enumerations;

namespace BokAdventure.Domain.Entities;

public class Bok : BaseAuditableEntity<BokIdentify>
{
    public string Name { get; set; } = string.Empty;
    public override BokIdentify Id { get; set; }
    public BokType Type { get; set; }
    public ulong HitPoints { get; set; } = 10;
    public long Defence { get; set; } = 1;
    public long Attack { get; set; } = 1;
}
