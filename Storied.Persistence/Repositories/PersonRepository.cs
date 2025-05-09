using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Storied.Application.Repositories;
using Storied.Domain.Entities;
using Storied.Persistence.Context;

namespace Storied.Persistence.Repositories
{
    /// <summary>  
    /// Repository implementation for managing <see cref="Person"/> entities.  
    /// </summary>  
    public class PersonRepository(StoriedContext context) : GenericRepository<Person>(context), IPersonRepository
    {
        /// <summary>  
        /// Retrieves all <see cref="Person"/> entities from the database.  
        /// </summary>  
        /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>  
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="Person"/> entities.</returns>  
        public Task<List<Person>> GetAllPersons(CancellationToken cancellationToken)
        {
            return GetAll().ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
