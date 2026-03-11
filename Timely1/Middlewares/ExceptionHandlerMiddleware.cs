using System.Net;

namespace Timely1.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next; // делегат куда пихать проверяемый запрос
        private readonly ILogger<ExceptionHandlerMiddleware> logger; // логгер

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context) // текущий запрос
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            logger.LogError($"Error {ex}");

            context.Response.StatusCode = (int)HttpStatusCode.BadRequest; // меняем код запроса на ошибку 400
            await context.Response.WriteAsJsonAsync(ex.Message); // и отдаём её передавая данные об ошибке
        }
    }
}
