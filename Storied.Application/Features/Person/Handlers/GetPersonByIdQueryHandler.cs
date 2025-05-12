using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Storied.Application.Common.Exceptions;
using Storied.Application.Features.Person.Queries;
using Storied.Application.Models;
using Storied.Application.Repositories;


namespace Storied.Application.Features.Person.Handlers;

/// <summary>  
/// Handles the query to retrieve a person by their unique identifier.  
/// </summary>  
/// <remarks>  
/// This handler uses the <see cref="IPersonRepository"/> to fetch the person entity  
/// and maps it to a <see cref="GetPersonByIdQueryResponse"/> using AutoMapper.  
/// </remarks>  
public sealed class GetPersonByIdQueryHandler : IRequestHandler<GetPersonByIdQuery, GetPersonByIdQueryResponse>
{
    private readonly IPersonRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetPersonByIdQueryHandler> _logger;

    /// <summary>  
    /// Initializes a new instance of the <see cref="GetPersonByIdQueryHandler"/> class.  
    /// </summary>  
    /// <param name="repository">The repository to access person data.</param>  
    /// <param name="mapper">The mapper to transform entities to DTOs.</param>  
    /// <param name="logger">The logger to log information and errors.</param>  
    public GetPersonByIdQueryHandler(IPersonRepository repository, IMapper mapper, ILogger<GetPersonByIdQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>  
    /// Handles the query to retrieve a person by their unique identifier.  
    /// </summary>  
    /// <param name="request">The query containing the person's unique identifier.</param>  
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>  
    /// <returns>A task that represents the asynchronous operation. The task result contains the response with person details.</returns>  
    /// <exception cref="NotFoundException">Thrown when a person with the specified ID is not found.</exception>  
    public async Task<GetPersonByIdQueryResponse> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetPersonByIdQuery for ID: {Id}", request.Id);

        var person = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _logger.LogInformation("Person with ID: {Id} retrieved successfully.", request.Id);
        return _mapper.Map<GetPersonByIdQueryResponse>(person);
    }
}
