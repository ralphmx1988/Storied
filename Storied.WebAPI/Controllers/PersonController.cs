using MediatR;
using Microsoft.AspNetCore.Mvc;
using Storied.Application.Features.Person.Commands;
using Storied.Application.Features.Person.Queries;
using Storied.Application.Models;

namespace Storied.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonController(IMediator mediator) : ControllerBase
{

    /// <summary>
    /// Retrieves all persons.
    /// </summary>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>A list of all employees.</returns>
    [HttpGet("GetAllPersons")]
    public async Task<ActionResult<List<GetAllPeopleQueryResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetAllPeopleQuery(), cancellationToken);
        return Ok(response);
    }


    /// <summary>
    /// Creates a new employee.
    /// </summary>
    /// <param name="request">The request containing employee details.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>The response containing the created employee's details.</returns>
    [HttpPost("AddPerson")]
    public async Task<ActionResult<AddPersonCommandResponse>> Create([FromBody]AddPersonCommand request,
        CancellationToken cancellationToken)
    {

        var response = await mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    /// <summary>
    /// Retrieves a person by ID.
    /// </summary>
    /// <param name="id">The ID of the person to retrieve.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>The details of the person.</returns>
    [HttpGet("GetPersonById/{id:guid}")]
    public async Task<ActionResult<GetPersonByIdQueryResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {

        var response = await mediator.Send(new GetPersonByIdQuery(id), cancellationToken);
        
        if (response == null)
        {
            return NotFound($"Person with ID {id} was not found.");
        }
        return Ok(response);
    }
    /// <summary>
    /// Updates an existing person.
    /// </summary>
    /// <param name="id">The ID of the person to update.</param>
    /// <param name="request">The request containing updated person details.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>The updated person's details.</returns>
    [HttpPut("RecordBirth/{id:guid}")]
    public async Task<ActionResult<RecordBirthCommandResponse>> AddRecordBirth(Guid id, [FromBody] RecordBirthCommand request, CancellationToken cancellationToken)
    {
        if (id != request.Id)
        {
            return BadRequest("Provided PersonId in the URL does not match PersonId in the request body.");
        }
        var response = await mediator.Send(request, cancellationToken);
        
        if (response == null)
        {
            return NotFound($"Person with Id {id} was not found to be updated.");
        }
        return Ok(response);
    }
}