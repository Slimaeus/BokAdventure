namespace BokAdventure.Domain.Common;
public interface IEntity<TId>
    where TId : notnull
{
    TId Id { get; }
}
