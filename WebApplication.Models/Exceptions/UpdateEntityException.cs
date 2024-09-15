using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models.Exceptions
{
    public class UpdateEntityException : WebApplication1ExceptionBase
    {
        public UpdateEntityException()
        {
        }

        public UpdateEntityException(Exception ex) : base(ex)
        {
        }

        public UpdateEntityException(string message) : base(message)
        {
        }

        public UpdateEntityException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UpdateEntityException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
