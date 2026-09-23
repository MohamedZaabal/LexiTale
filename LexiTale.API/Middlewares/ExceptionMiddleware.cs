namespace LexiTale.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";

                context.Response.StatusCode = ex.Message switch
                {
                    "Invalid email or password." => StatusCodes.Status401Unauthorized,
                    "Invalid Refresh Token." => StatusCodes.Status401Unauthorized,
                    "Refresh Token is revoked." => StatusCodes.Status401Unauthorized,
                    "Refresh Token expired." => StatusCodes.Status401Unauthorized,
                    "User with this email already exists." => StatusCodes.Status409Conflict,
                    "Word not found." => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status400BadRequest
                };

                await context.Response.WriteAsJsonAsync(new
                {
                    error = ex.Message
                });
            }
        }
    }
}