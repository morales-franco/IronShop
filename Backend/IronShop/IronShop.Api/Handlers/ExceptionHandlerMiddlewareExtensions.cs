using Microsoft.AspNetCore.Builder;

namespace IronShop.Api.Handlers
{
    //Declare extension method for ExceptionHandlerMiddleware --> For use it in startup.cs
    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseIronExceptionHandler(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
