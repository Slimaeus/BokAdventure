using BokAdventure.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace BokAdventure.Domain.Entities;
public sealed class ApplicationUser : IdentityUser<Guid>, IEntity<Guid>
{
    public Guid? PlayerId { get; set; }
    public Player? Player { get; set; }
}
