using MediatR;
using Storied.Application.Common.Enums;
using Storied.Application.Common.Exceptions;
using Storied.Application.Models;
using Storied.Application.Repositories;

namespace Storied.Application.Features.Person.Commands;

/// <summary>  
/// Command to update a person's details.  
/// </summary>  
public sealed record RecordBirthCommand : IRequest<RecordBirthCommandResponse>
{
    /// <summary>  
    /// Gets or sets the unique identifier of the person.  
    /// </summary>  
    public required Guid Id { get; set; }

    /// <summary>  
    /// Gets or sets the birthdate of the person.  
    /// </summary>  
    public string? BirthDate { get; set; }

    /// <summary>  
    /// Gets or sets the birth location of the person.  
    /// </summary>  
    public string? BirthLocation { get; set; }
}

