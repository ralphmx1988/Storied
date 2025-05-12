using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Storied.Application.Features.Person.Commands;
using Storied.Application.Models;
using Storied.Application.Repositories;

namespace Storied.Application.Features.Person.Handlers;

/// <summary>  
/// Handles the command to update a person's details.  
/// </summary>  
public sealed class RecordBirthCommandHandler : IRequestHandler<RecordBirthCommand, RecordBirthCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<RecordBirthCommandHandler> _logger;

    /// <summary>  
    /// Initializes a new instance of the <see cref="RecordBirthCommandHandler"/> class.  
    /// </summary>  
    /// <param name="unitOfWork">The unit of work to manage transactions.</param>  
    /// <param name="personRepository">The repository for managing person entities.</param>  
    /// <param name="mapper">The mapper for object-to-object mapping.</param>  
    /// <param name="logger">The logger for logging operations.</param>  
    public RecordBirthCommandHandler(IUnitOfWork unitOfWork, IPersonRepository personRepository, IMapper mapper, ILogger<RecordBirthCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _personRepository = personRepository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>  
    /// Handles the update person command.  
    /// </summary>  
    /// <param name="request">The command containing the person's updated details.</param>  
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>  
    /// <returns>A task that represents the asynchronous operation. The task result contains the response with updated person details.</returns>  
    public async Task<RecordBirthCommandResponse> Handle(RecordBirthCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling RecordBirthCommand for Person ID: {PersonId}", request.Id);

        var person = await _personRepository.GetByIdAsync(request.Id, cancellationToken);
        if (person == null)
        {
            _logger.LogWarning("Person with ID: {PersonId} not found.", request.Id);
            return null!;
        }

        _logger.LogInformation("Updating birth details for Person ID: {PersonId}", request.Id);
        person.BirthDate = Convert.ToDateTime(request.BirthDate);
        person.BirthLocation = request.BirthLocation;

        _personRepository.Update(person);
        await _unitOfWork.Save(cancellationToken);

        _logger.LogInformation("Successfully updated birth details for Person ID: {PersonId}", request.Id);
        return _mapper.Map<RecordBirthCommandResponse>(person);
    }
}
