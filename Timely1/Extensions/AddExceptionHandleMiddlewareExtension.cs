using Timely1.Middlewares;

namespace Timely1.Extensions
{
    public static class AddExceptionHandleMiddlewareExtension
    {
        public static void AddExceptionHandler(this WebApplication app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }

    }
}
