using MediatR;
using Storied.Application.Common.Enums;
using Storied.Application.Models;

namespace Storied.Application.Features.Person.Commands;

/// <summary>  
/// Command to add a new person.  
/// </summary>  
/// <param name="GivenName">The given name of the person.</param>  
/// <param name="SurName">The surname of the person.</param>  
/// <param name="Gender">The gender of the person.</param>  
/// <param name="BirthDate">The birthdate of the person.</param>  
/// <param name="BirthLocation">The birth location of the person.</param>  
/// <param name="DeathDate">The death date of the person.</param>  
/// <param name="DeathLocation">The death location of the person.</param>  
/// <returns>The unique identifier of the added person.</returns>  
public sealed record AddPersonCommand(
    
    string GivenName,
   string? SurName,
   string Gender,
   string? BirthDate,
   string? BirthLocation,
   string? DeathDate,
   string? DeathLocation)
   : IRequest<AddPersonCommandResponse>;
