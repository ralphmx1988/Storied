namespace Storied.Domain.Common;

/// <summary>
/// Represents the base entity with common properties for all entities.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    public Guid Id { get; set; }
}