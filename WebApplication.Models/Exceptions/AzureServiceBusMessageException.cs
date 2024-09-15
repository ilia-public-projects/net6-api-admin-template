using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models.Exceptions
{
    /// <summary>
    /// Exception thrown by azure when performing azure service bus operations
    /// </summary>
    public class AzureServiceBusMessageException : WebApplication1ExceptionBase
    {
        public AzureServiceBusMessageException()
        {
        }

        public AzureServiceBusMessageException(Exception ex) : base(ex)
        {
        }

        public AzureServiceBusMessageException(string message) : base(message)
        {
        }

        public AzureServiceBusMessageException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AzureServiceBusMessageException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
