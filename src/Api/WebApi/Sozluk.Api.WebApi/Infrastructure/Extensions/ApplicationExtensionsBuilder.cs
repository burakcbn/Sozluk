using Microsoft.AspNetCore.Diagnostics;
using System.Net.Mime;
using System.Net;
using Sozluk.Common.Infrastructure.Exceptions;
using Sozluk.Api.WebApi.Infrastructure.Results;

namespace Sozluk.Api.WebApi.Infrastructure.Extensions
{
    public static class ApplicationExtensionsBuilder
    {
        public static IApplicationBuilder ConfigureExceptionHandling(this IApplicationBuilder app,
                                                                     bool includeExceptionDetails = false,
                                                                     bool useDefaultHandligResponse = true,
                                                                     Func<HttpContext, Exception, Task> handleException = null)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(context =>
                {
                    var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();

                    if (!useDefaultHandligResponse && handleException == null)
                        throw new ArgumentException(nameof(handleException), $"{nameof(handleException)} cannot be null when{nameof(useDefaultHandligResponse)} is false");

                    if (!useDefaultHandligResponse && handleException != null)
                        return handleException(context, exceptionObject.Error);

                    return DefaultExceptionHandling(context, exceptionObject.Error, includeExceptionDetails);
                });
            });

            return app;
        }

        private static async Task DefaultExceptionHandling(HttpContext context, Exception exception, bool includeExceptionDetails)
        {

            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
            string message = "Internal server error occured";

            if (exception is UnauthorizedAccessException)
                httpStatusCode = HttpStatusCode.Unauthorized;

            if (exception is DatabaseValidationException)
            {

                var validationResponse = new ValidationResponseModel(exception.Message);
                await WriteResponse(context, httpStatusCode, validationResponse);
                return;
            }

            var response = new
            {
                HttpStatusCode = (int)httpStatusCode,
                Detail = includeExceptionDetails ? exception.ToString() : message,
            };

            await WriteResponse(context, httpStatusCode, response);



        }
        private static async Task WriteResponse(HttpContext context, HttpStatusCode httpStatusCode, object responseObj)
        {

            context.Response.StatusCode = (int)httpStatusCode;
            context.Response.ContentType = MediaTypeNames.Application.Json;

            await context.Response.WriteAsJsonAsync(responseObj);
        }
    }
}
