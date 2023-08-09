namespace Before
{
    public class MyMiddleware
    {
        private readonly RequestDelegate _next;

        public MyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            Console.WriteLine($"Запит {httpContext.Request.Method} до {httpContext.Request.Path} на порт {httpContext.Connection.LocalPort} обробляється.");

            await _next(httpContext);

            Console.WriteLine($"Запит {httpContext.Request.Method} до {httpContext.Request.Path} на порт {httpContext.Connection.LocalPort} оброблено.");
        }
    }
}
