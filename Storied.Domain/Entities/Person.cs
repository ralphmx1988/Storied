using Storied.Domain.Common;
using Storied.Domain.Common.Enums;
namespace Storied.Domain.Entities;

/// <summary>  
/// Represents a person entity with personal details and life events.  
/// </summary>  
public class Person : BaseEntity
{
    /// <summary>  
    /// Gets or sets the given name of the person.  
    /// </summary>  
    public required string GivenName { get; set; }

    /// <summary>  
    /// Gets or sets the surname of the person.  
    /// </summary>  
    public string? SurName { get; set; }

    /// <summary>  
    /// Gets or sets the gender of the person.  
    /// </summary>  
    public required Gender Gender { get; set; }

    /// <summary>  
    /// Gets or sets the birthdate of the person.  
    /// </summary>  
    public DateTime? BirthDate { get; set; }

    /// <summary>  
    /// Gets or sets the location where the person was born.  
    /// </summary>  
    public string? BirthLocation { get; set; }

    /// <summary>  
    /// Gets or sets the death date of the person, if applicable.  
    /// </summary>  
    public DateTime? DeathDate { get; set; }

    /// <summary>  
    /// Gets or sets the location where the person passed away, if applicable.  
    /// </summary>  
    public string? DeathLocation { get; set; }
}
