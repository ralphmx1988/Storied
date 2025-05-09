using AutoMapper;
using Storied.Application.Features.Person.Commands;
using Storied.Application.Models;

namespace Storied.Application.Mappers;

/// <summary>
/// Mapper profile for adding a person. Maps between <see cref="AddPersonCommand"/> and <see cref="Domain.Entities.Person"/>,
/// as well as between <see cref="Domain.Entities.Person"/> and <see cref="AddPersonCommandResponse"/>.
/// </summary>
public sealed class AddPersonCommandMapper : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AddPersonCommandMapper"/> class.
    /// Configures the mapping between <see cref="AddPersonCommand"/> and <see cref="Domain.Entities.Person"/>,
    /// and between <see cref="Domain.Entities.Person"/> and <see cref="AddPersonCommandResponse"/>.
    /// </summary>
    public AddPersonCommandMapper()
    {
        CreateMap<AddPersonCommand, Domain.Entities.Person>();
        CreateMap<Domain.Entities.Person, AddPersonCommandResponse>();
    }
}
