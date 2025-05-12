using MediatR;
using Storied.Application.Features.Person.Queries;
using Storied.Application.Models;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Storied.Application.Repositories;

namespace Storied.Application.Features.Person.Handlers;

/// <summary>  
/// Handles the query to retrieve all people.  
/// </summary>  
public sealed class GetAllPeopleQueryHandler : IRequestHandler<GetAllPeopleQuery, List<GetAllPeopleQueryResponse>>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllPeopleQueryHandler> _logger;

    /// <summary>  
    /// Initializes a new instance of the <see cref="GetAllPeopleQueryHandler"/> class.  
    /// </summary>  
    /// <param name="personRepository">The repository for accessing person data.</param>  
    /// <param name="mapper">The mapper for transforming entities to DTOs.</param>  
    /// <param name="logger">The logger for logging operations.</param>  
    public GetAllPeopleQueryHandler(IPersonRepository personRepository, IMapper mapper, ILogger<GetAllPeopleQueryHandler> logger)
    {
        _personRepository = personRepository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>  
    /// Handles the query to retrieve all people.  
    /// </summary>  
    /// <param name="request">The query request.</param>  
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>  
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of people responses.</returns>  
    public async Task<List<GetAllPeopleQueryResponse>> Handle(GetAllPeopleQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetAllPeopleQuery...");

           
        var persons = await _personRepository.GetAllPersons(cancellationToken: cancellationToken);
        _logger.LogInformation("Successfully retrieved {Count} persons.", persons.Count);

        return _mapper.Map<List<GetAllPeopleQueryResponse>>(persons);
           
    }
}