namespace Storied.WebAPI.Extensions;

/// <summary>
/// Extension methods for configuring CORS policy.
/// </summary>
public static class CorsPolicyExtensions
{
    /// <summary>
    /// Configures the CORS policy to allow any origin, method, and header.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the CORS policy to.</param>
    public static void ConfigureCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(opt =>
        {
            opt.AddDefaultPolicy(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        });
    }
}