
namespace Storied.Application.Models;

/// <summary>
/// Represents the response for the GetPersonById query.
/// </summary>
public sealed record GetPersonByIdQueryResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the person.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the given name of the person.
    /// </summary>
    public string? GivenName { get; set; }

    /// <summary>
    /// Gets or sets the surname of the person.
    /// </summary>
    public string? SurName { get; set; }

    /// <summary>
    /// Gets or sets the gender of the person.
    /// </summary>
    public string? Gender { get; set; }

    /// <summary>
    /// Gets or sets the birth date of the person.
    /// </summary>
    public DateTime? BirthDate { get; set; }

    /// <summary>
    /// Gets or sets the birth location of the person.
    /// </summary>
    public string? BirthLocation { get; set; }

    /// <summary>
    /// Gets or sets the death date of the person, if applicable.
    /// </summary>
    public DateTime? DeathDate { get; set; }

    /// <summary>
    /// Gets or sets the death location of the person, if applicable.
    /// </summary>
    public string? DeathLocation { get; set; }
}
