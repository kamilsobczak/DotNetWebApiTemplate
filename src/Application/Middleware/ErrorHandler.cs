using Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Middleware
{
    public class ErrorHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ErrorHandler(RequestDelegate next, ILogger<ErrorHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var response = httpContext.Response;
                response.ContentType = "application/json";

                switch (ex)
                {

                    case AppException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled error
                        _logger.LogError("Unexpected error: {0}", ex.Message);
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }


                var result = JsonSerializer.Serialize(new { message = ex?.Message });

                await HandleExceptionAsync(response, ex);
            }
        }

        private Task HandleExceptionAsync(HttpResponse response, Exception? ex)
        {
            return response.WriteAsync(new ErrorModel()
            {
                StatusCode = response.StatusCode,
                Message = ex.Message,
            }.ToString());
        }
    }
}