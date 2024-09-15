using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models.Exceptions
{
    public class WebApplication1UnauthorizedException : Exception
    {
        public WebApplication1UnauthorizedException()
        {
        }

        public WebApplication1UnauthorizedException(string message) : base(message)
        {
        }

        public WebApplication1UnauthorizedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WebApplication1UnauthorizedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
