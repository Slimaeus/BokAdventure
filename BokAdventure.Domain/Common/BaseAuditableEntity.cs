namespace BokAdventure.Domain.Common;
public abstract class BaseAuditableEntity<TId> : IAuditableEntity<TId>
    where TId : notnull
{
    public abstract TId Id { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime ModifiedDate { get; set; }
}

public abstract class BaseAuditableEntity : BaseEntity<Guid>
{
    public override Guid Id { get; set; } = Guid.NewGuid();
}

public abstract class BaseIntKeyAuditableEntity : BaseEntity<int>
{
    public override int Id { get; set; }
}
