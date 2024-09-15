using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models.Exceptions
{
    public class WebApplication1ValidationException : Exception
    {
        public WebApplication1ValidationException(string message, List<string> errors) : base(message)
        {
            Errors = errors;
        }

        public WebApplication1ValidationException(string message) : base(message)
        {
        }

        public WebApplication1ValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WebApplication1ValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public List<string> Errors { get; private set; }
    }
}
