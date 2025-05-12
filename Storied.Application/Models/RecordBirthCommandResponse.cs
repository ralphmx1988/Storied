namespace Storied.Application.Models;

/// <summary>
/// Represents the response for updating a person's information.
/// </summary>
public sealed record RecordBirthCommandResponse
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
    /// Gets or sets the birthdate of the person.
    /// </summary>
    public DateTime? BirthDate { get; set; }

    /// <summary>
    /// Gets or sets the birth location of the person.
    /// </summary>
    public string? BirthLocation { get; set; }
}
