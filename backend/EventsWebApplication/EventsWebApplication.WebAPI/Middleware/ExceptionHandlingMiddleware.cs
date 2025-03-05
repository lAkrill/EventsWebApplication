using EventsWebApplication.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text.Json;

namespace EventsWebApplication.WebAPI.Middleware
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (NotFoundException ex)
            {
                await context.Response.WriteAsync(GenerateErrorDetails(context, ex, StatusCodes.Status404NotFound));
            } 
            catch (NotUniqueException ex)
            {
                await context.Response.WriteAsync(GenerateErrorDetails(context, ex, StatusCodes.Status400BadRequest));
            }
            catch (InvalidPaginationException ex)
            {
                await context.Response.WriteAsync(GenerateErrorDetails(context, ex, StatusCodes.Status400BadRequest));
            }

            catch (InvalidPropertyAdditionException ex)
            {
                await context.Response.WriteAsync(GenerateErrorDetails(context, ex, StatusCodes.Status400BadRequest));
            }
            catch (LoginException ex)
            {
                await context.Response.WriteAsync(GenerateErrorDetails(context, ex, StatusCodes.Status401Unauthorized));
            }
            catch (ArgumentException ex)
            {
                await context.Response.WriteAsync(GenerateErrorDetails(context, ex, StatusCodes.Status400BadRequest));
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred: {ex.Message}");

                await context.Response.WriteAsync(GenerateErrorDetails(context, ex, StatusCodes.Status500InternalServerError));
            }
        }

        private string GenerateErrorDetails(HttpContext context, Exception ex, int statusCode)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json+error";

            var errorDetails = new ProblemDetails
            {
                Status = statusCode,
                Detail = ex.Message,
                Instance = context.Request.Path
            };

            return JsonSerializer.Serialize(errorDetails);
        }
    }
}
