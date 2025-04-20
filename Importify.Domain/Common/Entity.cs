namespace Importify.Domain.Common;

/// <summary>
/// Abstract base class representing an entity with a unique identifier.
/// </summary>
public abstract class Entity
{
    protected Entity(Guid id)
    {
        Id = id;
    }

    /// <summary>
    /// Unique identifier of the entity.
    /// </summary>
    public Guid Id { get; init; }
}