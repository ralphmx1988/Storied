using Storied.Domain.Common;
using Storied.Application.Repositories;
using Storied.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Storied.Persistence.Repositories;

/// <summary>  
/// A generic repository implementation for performing CRUD operations on entities.  
/// </summary>  
/// <typeparam name="T">The type of the entity.</typeparam>  
public class GenericRepository<T>(StoriedContext context) : IGenericRepository<T>
    where T : BaseEntity
{

    protected readonly StoriedContext Context = context;


    /// <summary>  
    /// Retrieves all entities as an <see cref="IQueryable{T}"/>.  
    /// </summary>  
    /// <returns>An <see cref="IQueryable{T}"/> representing all entities.</returns>  
    public IQueryable<T> GetAll()
    {
        return Context.Set<T>().AsQueryable();
    }

    /// <summary>  
    /// Retrieves an entity by its identifier asynchronously.  
    /// </summary>  
    /// <param name="id">The identifier of the entity.</param>  
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>  
    /// <returns>A task that represents the asynchronous operation. The task result contains the entity if found; otherwise, <c>null</c>.</returns>  
    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await Context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    /// <summary>  
    /// Adds a new entity asynchronously.  
    /// </summary>  
    /// <param name="entity">The entity to add.</param>  
    /// <returns>A task that represents the asynchronous operation. The task result contains the added entity.</returns>  
    public async Task<T> AddAsync(T entity)
    {
        var entry = await Context.Set<T>().AddAsync(entity);
        return entry.Entity;
    }

    /// <summary>  
    /// Updates an existing entity.  
    /// </summary>  
    /// <param name="entity">The entity to update.</param>  
    public void Update(T entity)
    {
        Context.Entry(entity).State = EntityState.Modified;
    }

    /// <summary>  
    /// Deletes an existing entity by its identifier asynchronously.  
    /// </summary>  
    /// <param name="id">The identifier of the entity to delete.</param>  
    /// <returns>A task that represents the asynchronous operation. The task result contains the deleted entity if found; otherwise, <c>null</c>.</returns>  
    public async Task<T?> DeleteAsync(Guid id)
    {
        var entity = await Context.Set<T>().FindAsync(id);
        if (entity == null)
        {
            return null;
        }
        Context.Set<T>().Remove(entity);
        return entity;
    }

}