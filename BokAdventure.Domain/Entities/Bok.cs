using BokAdventure.Domain.Common;
using BokAdventure.Domain.Enumerations;

namespace BokAdventure.Domain.Entities;

public class Bok : BaseAuditableEntity
{
    public BokType Type { get; set; }
    public Guid? PlayerId { get; set; }
    public Player? Player { get; set; }
}
