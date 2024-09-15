using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApplication1.API.Extensions;
using WebApplication1.DTO;
using WebApplication1.Models.Common;
using WebApplication1.Services;

namespace WebApplication1.RequestFilters
{
    /// <summary></summary>
    public class GlobalRequestFilter : IActionFilter
    {
        private readonly ILogger<GlobalRequestFilter> logger;
        private readonly IOperationContextFactory operationContextFactory;

        /// <summary></summary>
        public GlobalRequestFilter(
            ILogger<GlobalRequestFilter> logger,
            IOperationContextFactory operationContextFactory
            )
        {
            this.logger = logger;
            this.operationContextFactory = operationContextFactory;
        }

        /// <summary></summary>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            IOperationContext operationContext = context.HttpContext.GetOperationContext();
            if (null != operationContext)
            {
                logger.LogInformation($"END,{operationContext.OperationId},{context.HttpContext.Request.Method},{context.HttpContext.Request.Path},{context.HttpContext.Request.QueryString}");
            }

            context.HandleException(operationContext, logger);
        }

        /// <summary></summary>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            IOperationContext operationContext = operationContextFactory.CreateOperationContext(context.HttpContext.Request);
            context.HttpContext.SetOperationContext(operationContext);

            logger.LogInformation($"START,{operationContext.OperationId},{context.HttpContext.Request.Method},{context.HttpContext.Request.Path},{context.HttpContext.Request.QueryString}");
        }
    }
}
