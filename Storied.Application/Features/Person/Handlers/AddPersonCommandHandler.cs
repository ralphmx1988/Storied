using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Storied.Application.Features.Person.Commands;
using Storied.Application.Models;
using Storied.Application.Repositories;

namespace Storied.Application.Features.Person.Handlers
{
    /// <summary>  
    /// Handles the addition of a new person by processing the <see cref="AddPersonCommand"/>.  
    /// </summary>  
    /// <param name="unitOfWork">The unit of work to manage database transactions.</param>
    /// <param name="mapper">The mapper for converting between models and entities.</param>  
    public sealed class AddPersonCommandHandler(
       IUnitOfWork unitOfWork,
       IPersonRepository personRepository,
       IMapper mapper,
       ILogger<AddPersonCommandHandler> logger)
       : IRequestHandler<AddPersonCommand, AddPersonCommandResponse>
    {
        /// <summary>  
        /// Handles the command to add a new person.  
        /// </summary>  
        /// <param name="request">The command containing the details of the person to add.</param>  
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>  
        /// <returns>A task that represents the asynchronous operation. The task result contains the response with the added person's details.</returns>  
        public async Task<AddPersonCommandResponse> Handle(AddPersonCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling AddPersonCommand for GivenName: {GivenName}, SurName: {SurName}", request.GivenName, request.SurName);

            var person = mapper.Map<Domain.Entities.Person>(request);

            logger.LogDebug("Mapped AddPersonCommand to Person entity: {@Person}", person);

            await personRepository.AddAsync(person);
            logger.LogInformation("Person entity added to repository: {PersonId}", person.Id);

            await unitOfWork.Save(cancellationToken);
            logger.LogInformation("UnitOfWork changes saved for PersonId: {PersonId}", person.Id);

            var response = mapper.Map<AddPersonCommandResponse>(person);
            logger.LogDebug("Mapped Person entity to AddPersonCommandResponse: {@Response}", response);

            return response;
        }
    }
}
