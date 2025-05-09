using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Storied.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Storied.Application.Repositories;
using Storied.Persistence.Repositories;

namespace Storied.Persistence.Extensions
{
    /// <summary>  
    /// Provides extension methods for configuring services related to persistence.  
    /// </summary>  
    public static class ServiceExtensions
    {
        /// <summary>  
        /// Configures the persistence services for the application.  
        /// </summary>  
        /// <param name="services">The service collection to add the services to.</param>  
        /// <param name="configuration">The configuration to use for retrieving the connection string.</param>  
        public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Sql");

            services.AddDbContext<StoriedContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IPersonRepository, PersonRepository>();


        }
    }
}
