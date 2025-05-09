namespace Storied.Application.Repositories;

/// <summary>
/// Represents a unit of work that can save changes to the data store.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Saves all changes made in this unit of work.
    /// </summary>
    /// <returns>A task that represents the asynchronous save operation.</returns>
    Task Save(CancellationToken cancellationToken);
}