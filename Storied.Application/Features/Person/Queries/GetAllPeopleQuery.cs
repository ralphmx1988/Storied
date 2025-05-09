using MediatR;
using Storied.Application.Models;

namespace Storied.Application.Features.Person.Queries
{
    /// <summary>  
    /// Represents a query to retrieve all people.  
    /// </summary>  
    public sealed record GetAllPeopleQuery : IRequest<List<GetAllPeopleQueryResponse>>;  
}
