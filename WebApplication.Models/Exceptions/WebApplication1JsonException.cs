using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models.Exceptions
{
    /// <summary>
    /// Exception thrown when failing to deserialize json
    /// </summary>
    public class WebApplication1JsonException : WebApplication1ExceptionBase
    {
        public WebApplication1JsonException()
        {
        }

        public WebApplication1JsonException(Exception ex) : base(ex)
        {
        }

        public WebApplication1JsonException(string message) : base(message)
        {
        }

        public WebApplication1JsonException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WebApplication1JsonException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
