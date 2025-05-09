using MediatR;
using Storied.Application.Models;

namespace Storied.Application.Features.Person.Queries;

/// <summary>  
/// Represents a query to retrieve a person by their unique identifier.  
/// </summary>  
/// <param name="Id">The unique identifier of the person to retrieve.</param>  
/// <returns>A response containing the details of the person.</returns>  
public sealed record GetPersonByIdQuery(Guid Id) : IRequest<GetPersonByIdQueryResponse>;
