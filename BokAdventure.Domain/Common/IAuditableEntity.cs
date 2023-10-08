namespace BokAdventure.Domain.Common;
public interface IAuditableEntity<TId> : IEntity<TId>, IAuditable
    where TId : notnull
{
}
