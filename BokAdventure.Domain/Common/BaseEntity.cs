namespace BokAdventure.Domain.Common;
public abstract class BaseEntity<TId> : IEntity<TId>
    where TId : notnull
{
    public abstract TId Id { get; protected set; }
}

public abstract class BaseEntity : BaseEntity<Guid>
{
    public override Guid Id { get; protected set; } = Guid.NewGuid();
}
