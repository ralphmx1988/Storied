using Microsoft.AspNetCore.Mvc;

namespace Storied.WebAPI.Extensions;

/// <summary>
/// Provides extension methods for configuring API behavior.
/// </summary>
public static class ApiBehaviorExtensions
{
    /// <summary>
    /// Configures the API behavior options for the application.
    /// </summary>
    /// <param name="services">The service collection to add the configuration to.</param>
    public static void ConfigureApiBehavior(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
    }
}