using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models.Exceptions
{
    public class DeleteEntityException : WebApplication1ExceptionBase
    {
        public DeleteEntityException()
        {
        }

        public DeleteEntityException(Exception ex) : base(ex)
        {
        }

        public DeleteEntityException(string message) : base(message)
        {
        }

        public DeleteEntityException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DeleteEntityException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
