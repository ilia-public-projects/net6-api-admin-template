using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models.Exceptions
{
    /// <summary>
    /// Exception thrown when the REST response is not 200
    /// </summary>
    public class RestBadRequestException : WebApplication1ExceptionBase
    {
        public RestBadRequestException()
        {
        }

        public RestBadRequestException(Exception ex) : base(ex)
        {
        }

        public RestBadRequestException(string message) : base(message)
        {
        }

        public RestBadRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RestBadRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
