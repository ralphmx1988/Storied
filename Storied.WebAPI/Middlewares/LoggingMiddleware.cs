namespace Storied.WebAPI.Middlewares
{
    /// <summary>
    /// Middleware for logging the execution of HTTP requests, including controller, action, and query parameters.
    /// </summary>
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        /// <param name="logger">The logger instance for logging information.</param>
        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Invokes the middleware to log the execution of the HTTP request.
        /// </summary>
        /// <param name="context">The HTTP context of the current request.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="context"/> is null.</exception>
        public async Task InvokeAsync(HttpContext? context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context), "HttpContext cannot be null.");
            }

            var controllerName = context.GetRouteValue("controller")?.ToString();
            var actionName = context.GetRouteValue("action")?.ToString();
            var parameters = context.Request.Query.Select(p => $"{p.Key}={p.Value}").ToList();

            _logger.LogInformation($"{controllerName}: {actionName}: {string.Join(", ", parameters)} execution started.");

            await _next.Invoke(context);

            _logger.LogInformation($"{controllerName}: {actionName}: {string.Join(", ", parameters)} execution done.");
        }
    }
}
