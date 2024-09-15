using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTO;
using WebApplication1.Services;
using WebApplication1.Models.Exceptions;
using WebApplication1.Services.Utils;

namespace WebApplication1.API.Extensions
{
    /// <summary></summary>
    public static class ActionExecutedContextContextExtensions
    {
        /// <summary>
        /// When exception occurs, validation exceptions and not found exceptions are returned as 400, all other exceptions and unhandled exceptions are 500
        /// </summary>
        /// <remarks>
        /// In minor cases an unauthorized exception can be returned which can then be converted to a 401
        /// </remarks>
        /// <param name="context">Action executed context</param>
        /// <param name="operationContext">Request operation context</param>
        /// <param name="logger">Logger</param>
        public static void HandleException(this ActionExecutedContext context, IOperationContext operationContext, ILogger logger)
        {
            if (context.Exception != null)
            {
                if (context.Exception is WebApplication1UnauthorizedException)
                {
                    context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                }
                else if (context.Exception is WebApplication1ValidationException)
                {
                    WebApplication1ValidationException exception = context.Exception as WebApplication1ValidationException;
                    APIResponse<string> apiResponse = new APIResponse<string>($"Validation failed!", exception.Errors?.ToArray() ?? new string[] { });
                    context.Result = new BadRequestObjectResult(apiResponse);
                }
                else if (context.Exception is WebApplication1NotFoundExceptionBase)
                {
                    WebApplication1ValidationException exception = context.Exception as WebApplication1ValidationException;
                    APIResponse<string> apiResponse = new APIResponse<string>($"Not found!", exception.Errors?.ToArray() ?? new string[] { "Entity not found" });
                    context.Result = new BadRequestObjectResult(apiResponse);
                }
                else
                {
                    string error = "An error occured processing operation";
                    if (context.Exception is WebApplication1ExceptionBase)
                    {
                        WebApplication1ExceptionBase exception = context.Exception as WebApplication1ExceptionBase;
                        ErrorUtils.LogException(operationContext, logger, exception, $"An error occured in operation, message: {exception.Message}");
                    }
                    else
                    {
                        error = "Unhandled exception in operation";
                        ErrorUtils.LogException(operationContext, logger, context.Exception, $"Unhandled exception in operation, message: {context.Exception.Message}");
                    }

                    APIResponse<string> apiResponse = new APIResponse<string>($"{error}");

                    context.Result = new JsonResult(apiResponse) { StatusCode = StatusCodes.Status500InternalServerError };
                }

                context.Exception = null;
            }
        }
    }
}
