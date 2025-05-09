using Storied.Application.Repositories;
using Storied.Persistence.Context;

namespace Storied.Persistence.Repositories;

/// <summary>  
/// Represents the unit of work implementation for managing database transactions.  
/// </summary>  
public class UnitOfWork : IUnitOfWork
{
    private readonly StoriedContext _context;

    /// <summary>  
    /// Initializes a new instance of the <see cref="UnitOfWork"/> class.  
    /// </summary>  
    /// <param name="context">The database context to be used by this unit of work.</param>  
    public UnitOfWork(StoriedContext context)
    {
        _context = context;
    }

    /// <summary>  
    /// Saves all changes made in this unit of work to the data store.  
    /// </summary>  
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>  
    /// <returns>A task that represents the asynchronous save operation.</returns>  
    public Task Save(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}