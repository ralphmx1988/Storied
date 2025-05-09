using AutoMapper;
using MediatR;
using Storied.Application.Features.Person.Commands;
using Storied.Application.Models;
using Storied.Application.Repositories;

namespace Storied.Application.Features.Person.Handlers;

/// <summary>  
/// Handles the command to update a person's details.  
/// </summary>  
public sealed class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, UpdatePersonCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    /// <summary>  
    /// Initializes a new instance of the <see cref="UpdatePersonCommandHandler"/> class.  
    /// </summary>  
    /// <param name="unitOfWork">The unit of work to manage transactions.</param>  
    /// <param name="personRepository">The repository for managing person entities.</param>  
    /// <param name="mapper">The mapper for object-to-object mapping.</param>  
    public UpdatePersonCommandHandler(IUnitOfWork unitOfWork, IPersonRepository personRepository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _personRepository = personRepository;
        _mapper = mapper;
    }

    /// <summary>  
    /// Handles the update person command.  
    /// </summary>  
    /// <param name="request">The command containing the person's updated details.</param>  
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>  
    /// <returns>A task that represents the asynchronous operation. The task result contains the response with updated person details.</returns>  
    public async Task<UpdatePersonCommandResponse> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {

        var person = await _personRepository.GetByIdAsync(request.Id, cancellationToken);
        if (person == null)
        {
            return null!;
        }

        person.BirthDate = Convert.ToDateTime(request.BirthDate);
        person.BirthLocation = request.BirthLocation;

        _personRepository.Update(person);
        await _unitOfWork.Save(cancellationToken);

        return _mapper.Map<UpdatePersonCommandResponse>(person);
    }
}
