using System;
using System.Diagnostics;
using System.Net;
using MovieStoreApi.Services;
using Newtonsoft.Json;

namespace MovieStoreApi.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _loggerService;
        public CustomExceptionMiddleware(RequestDelegate next, ILoggerService loggerService)
        {
            _next = next;
            _loggerService = loggerService;
        }
        public async Task Invoke(HttpContext Context)
        {
            var watch = Stopwatch.StartNew();
            try
            {
                string message = "[Request] HTTP " + Context.Request.Method + "-" + Context.Request.Path;
                _loggerService.write(message);
                await _next(Context);
                watch.Stop();
                message = "[Response] HTTP " + Context.Request.Method + "-" + Context.Request.Path + " responded " + Context.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds + " ms ";
                _loggerService.write(message);
            }
            catch(Exception ex)
            {
                watch.Stop();
                await HandleException(Context, ex, watch);
            }
        }
        private Task HandleException(HttpContext Context, Exception ex, Stopwatch watch)
        {
            Context.Response.ContentType = "Application/json";
            Context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            string message = "[ERROR] HTTP" + Context.Request.Method + " - " + Context.Response.StatusCode + "Error Message" + ex.Message + "in" + watch.Elapsed.TotalMilliseconds + "ms";
            _loggerService.write(message);


            var result = JsonConvert.SerializeObject(new { error = ex.Message }, Formatting.None);

            return Context.Response.WriteAsync(result);
        }
    }
    public static class CustomExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}

