using Storied.Domain.Entities;

namespace Storied.Application.Repositories
{


    /// <summary>  
    /// Repository interface for managing <see cref="Person"/> entities.  
    /// </summary>  
    public interface IPersonRepository : IGenericRepository<Person>
    {

        /// <summary>
        /// Gets an employee by their email address.
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the employee entity.</returns>
        Task<List<Person>> GetAllPersons(CancellationToken cancellationToken);
    }
}
