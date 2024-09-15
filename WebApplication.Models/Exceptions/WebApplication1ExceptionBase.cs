using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models.Exceptions
{
    public class WebApplication1ExceptionBase : Exception
    {
        public WebApplication1ExceptionBase()
        {
        }

        public WebApplication1ExceptionBase(Exception ex) : base(ex.Message, ex) { }

        public WebApplication1ExceptionBase(string message) : base(message)
        {
        }

        public WebApplication1ExceptionBase(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WebApplication1ExceptionBase(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
