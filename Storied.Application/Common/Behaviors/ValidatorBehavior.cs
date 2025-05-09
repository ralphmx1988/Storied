using FluentValidation;
using MediatR;
using Storied.Application.Common.Exceptions;

namespace Storied.Application.Common.Behaviors;

/// <summary>  
/// Represents a pipeline behavior for validating requests using FluentValidation.  
/// </summary>  
/// <typeparam name="TRequest">The type of the request.</typeparam>  
/// <typeparam name="TResponse">The type of the response.</typeparam>  
/// <param name="validators">A collection of validators for the request type.</param>  
public sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
   where TRequest : IRequest<TResponse>
{
    /// <summary>  
    /// Handles the validation of the incoming request and invokes the next delegate in the pipeline.  
    /// </summary>  
    /// <param name="request">The incoming request to validate.</param>  
    /// <param name="next">The next delegate in the pipeline.</param>  
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>  
    /// <returns>The response from the next delegate in the pipeline.</returns>  
    /// <exception cref="BadRequestException">Thrown when validation errors are found.</exception>  
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!validators.Any()) return await next(cancellationToken);

        var context = new ValidationContext<TRequest>(request);

        var errors = validators
            .Select(x => x.Validate(context))
            .SelectMany(x => x.Errors)
            .Where(x => x != null)
            .Select(x => x.ErrorMessage)
            .Distinct()
            .ToArray();

        if (errors.Any())
            throw new BadRequestException(errors);

        return await next(cancellationToken);
    }
}
