using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using RecipeBook.SharedKernel.Exceptions;
using System.Net;
using System.Text.Json;

namespace RecipeBook.ApiGateway.Api.Configuration
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();
                    if (exceptionObject is not null)
                    {
                        var errorDetails = exceptionObject.Error switch
                        {
                            NotFoundException ex => new ErrorDetails((int)HttpStatusCode.NotFound, ex.Message),
                            AppException ex => new ErrorDetails((int)HttpStatusCode.BadRequest, ex.Message),
                            _ => new ErrorDetails((int)HttpStatusCode.BadRequest, "System error!")
                        };

                        context.Response.ContentType = "application/problem+json; charset=utf-8";
                        context.Response.StatusCode = errorDetails.StatusCode;
                        await context.Response.WriteAsync(errorDetails.ToString());
                    }
                });
            });
        }
    }

    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ErrorDetails(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
