using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;
using Storied.Application.Common.Exceptions;

namespace Storied.WebAPI.Extensions;

public static class ErrorHandlerExtensions
{
    /// <summary>
    /// Configures the application to use a custom error handler.
    /// </summary>
    /// <param name="app">The application builder.</param>
    public static void UseErrorHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature == null) return;

                context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
                context.Response.ContentType = "application/json";


                context.Response.StatusCode = contextFeature.Error switch
                {
                    BadRequestException => (int)HttpStatusCode.BadRequest,
                    OperationCanceledException => (int)HttpStatusCode.ServiceUnavailable,
                    NotFoundException => (int)HttpStatusCode.NotFound,
                    _ => (int)HttpStatusCode.InternalServerError
                };

                string[]? errors = contextFeature.Error switch
                {
                    BadRequestException => ((BadRequestException)contextFeature.Error.GetBaseException()).Errors,
                    _ => throw new ArgumentOutOfRangeException()
                };

                var errorResponse = new
                {
                    statusCode = context.Response.StatusCode,
                    message = contextFeature.Error.GetBaseException().Message,
                    errors
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
            });
        });
    }
}