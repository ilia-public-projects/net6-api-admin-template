using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models.Exceptions
{
    public class WebApplication1NotFoundExceptionBase : Exception
    {
        public WebApplication1NotFoundExceptionBase()
        {
        }

        public WebApplication1NotFoundExceptionBase(string message) : base(message)
        {
        }

        public WebApplication1NotFoundExceptionBase(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WebApplication1NotFoundExceptionBase(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
