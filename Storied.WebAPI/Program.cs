
using Serilog;
using Storied.Application.Extensions;
using Storied.Persistence.Context;
using Storied.Persistence.Extensions;
using Storied.WebAPI.Extensions;
using Storied.WebAPI.Middlewares;
using System.Text.Json.Serialization;

namespace Storied.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.


            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            builder.Services.ConfigurePersistence(builder.Configuration);
            builder.Services.ConfigureApplication();
            builder.Services.ConfigureApiBehavior();
            builder.Services.ConfigureCorsPolicy();



            // Fix: Use the Serilog.Extensions.Hosting package to enable UseSerilog
            builder.Host.UseSerilog((context, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            var serviceScope = app.Services.CreateScope();
            var dataContext = serviceScope.ServiceProvider.GetService<StoriedContext>();
            dataContext?.Database.EnsureCreated();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

           


            app.UseMiddleware<LoggingMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseErrorHandler();
            app.UseCors();
            app.MapControllers();
            app.Run();

            app.Run();
        }
    }
}
