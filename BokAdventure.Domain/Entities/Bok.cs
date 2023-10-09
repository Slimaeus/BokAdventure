using BokAdventure.Domain.Common;
using BokAdventure.Domain.Enumerations;

namespace BokAdventure.Domain.Entities;

public class Bok : BaseAuditableEntity
{
    public BokType Type { get; set; }
}
