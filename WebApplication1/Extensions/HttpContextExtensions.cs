using WebApplication1.Services;

namespace WebApplication1.API.Extensions
{
    /// <summary></summary>
    public static class HttpContextExtensions
    {
        const string ActionItemOperationContext = "ActionItemOperationContext";

        /// <summary></summary>
        public static IOperationContext GetOperationContext(this HttpContext context)
        {
            return context.Items[ActionItemOperationContext] as IOperationContext;
        }

        /// <summary></summary>
        public static void SetOperationContext(this HttpContext context, IOperationContext operationContext)
        {
            context.Items[ActionItemOperationContext] = operationContext;
        }
    }
}
