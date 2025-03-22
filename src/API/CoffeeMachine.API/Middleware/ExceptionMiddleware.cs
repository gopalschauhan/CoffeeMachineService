namespace CoffeeMachine.API.Middleware
{
    public class ExceptionMiddleware
    {
        public readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            switch (ex)
            {
                case FirstDayOfAprilException firstDayOfAprilException:
                    httpContext.Response.StatusCode = StatusCodes.Status418ImATeapot;
                    break;
                default:
                    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    var problem = new ProblemDetails
                    {
                        Title = ex.Message,
                        Status = StatusCodes.Status500InternalServerError,
                        Type = nameof(HttpStatusCode.InternalServerError),
                        Detail = ex.StackTrace,
                    };
                    await httpContext.Response.WriteAsJsonAsync(problem);
                    break;
            }
        }
    }
}
