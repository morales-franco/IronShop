using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace IronShop.Api.Handlers
{
    //Declare middleware
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger _logger = Log.ForContext<ExceptionHandlerMiddleware>();
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            try
            {
                // Call the next delegate/middleware in the pipeline
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
                
            }

            
        }

        private  Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.Error(ex, "########## Handled Error ##########");

            var code = HttpStatusCode.InternalServerError;

            //if (exception is MyNotFoundException) code = HttpStatusCode.NotFound;
            //else if (exception is MyUnauthorizedException) code = HttpStatusCode.Unauthorized;
            //else if (exception is MyException) code = HttpStatusCode.BadRequest;

            var result = JsonConvert.SerializeObject(new
            {
                error = ex.Message
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);

        }
    }
}
