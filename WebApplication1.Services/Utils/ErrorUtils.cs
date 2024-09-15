using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Services.Utils
{
    /// <summary>
    /// Utils used for handling errors
    /// </summary>
    public static class ErrorUtils
    {
        /// <summary>
        /// Logs details of the exception and current operation context and throws the exception
        /// </summary>
        /// <param name="operationContext">Request operation context</param>
        /// <param name="logger">Logger</param>
        /// <param name="message">Custom message</param>
        /// <param name="throwException">Action used to throw the exception</param>
        public static void LogAndThrowException(IOperationContext operationContext, ILogger logger, string message, Action throwException)
        {
            logger.LogError($"{operationContext.OperationId}:{message}");
            throwException();
        }

        /// <summary>
        /// Logs details of the exception and current operation context
        /// </summary>
        /// <param name="operationContext">Request operation context</param>
        /// <param name="logger">Logger</param>
        /// <param name="message">Custom message</param>
        public static void LogException(IOperationContext operationContext, ILogger logger, Exception ex, string message)
        {
            logger.LogError(ex, $"{operationContext.OperationId}:{message}");
        }
    }
}
