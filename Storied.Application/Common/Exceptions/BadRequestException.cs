namespace Storied.Application.Common.Exceptions;

/// <summary>
/// Represents errors that occur during application execution due to a bad request.
/// </summary>
public class BadRequestException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BadRequestException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public BadRequestException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BadRequestException"/> class with a specified array of error messages.
    /// </summary>
    /// <param name="errors">The array of error messages that describe the errors.</param>
    public BadRequestException(string[] errors) : base("Validation Errors, see errors detail.")
    {
        Errors = errors;
    }

    /// <summary>
    /// Gets or sets the array of error messages.
    /// </summary>
    public string[]? Errors { get; set; }
}