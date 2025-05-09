using Storied.Domain.Common;

namespace Storied.Application.Repositories;

/// <summary>
/// Generic repository interface for CRUD operations.
/// </summary>
/// <typeparam name="T">The type of the entity.</typeparam>
public interface IGenericRepository<T> where T : BaseEntity
{
    /// <summary>
    /// Retrieves all entities as an <see cref="IQueryable{T}"/>.
    /// </summary>
    /// <returns>An <see cref="IQueryable{T}"/> representing all entities.</returns>
    IQueryable<T> GetAll();

    /// <summary>
    /// Retrieves an entity by its identifier asynchronously.
    /// </summary>
    /// <param name="id">The identifier of the entity.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the entity if found; otherwise, <c>null</c>.</returns>
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Adds a new entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task<T> AddAsync(T entity);

    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    void Update(T entity);

    /// <summary>
    /// Deletes an existing entity.
    /// </summary>
    /// <param name="id"></param>
    Task<T?> DeleteAsync(Guid id);
}