namespace Storied.Application.Common.Exceptions;

/// <summary>
/// Exception that is thrown when an attempt to access a resource that does not exist is made.
/// </summary>
public class NotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public NotFoundException(string message) : base(message)
    {
    }
}