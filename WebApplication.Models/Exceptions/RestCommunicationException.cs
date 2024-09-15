using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models.Exceptions
{
    /// <summary>
    /// Exception thrown when an exception occured while sending REST request
    /// </summary>
    public class RestCommunicationException : WebApplication1ExceptionBase
    {
        public RestCommunicationException()
        {
        }

        public RestCommunicationException(Exception ex) : base(ex)
        {
        }

        public RestCommunicationException(string message) : base(message)
        {
        }

        public RestCommunicationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RestCommunicationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
