using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Storied.Application.Common.Behaviors;
using System.Reflection;
using FluentValidation;

namespace Storied.Application.Extensions;

/// <summary>  
/// Provides extension methods for configuring application services.  
/// </summary>  
public static class ServiceExtensions
{
    /// <summary>  
    /// Configures application services by registering AutoMapper, MediatR, FluentValidation,  
    /// and pipeline behaviors into the dependency injection container.  
    /// </summary>  
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>  
    public static void ConfigureApplication(this IServiceCollection services)
    {
       
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }
}
