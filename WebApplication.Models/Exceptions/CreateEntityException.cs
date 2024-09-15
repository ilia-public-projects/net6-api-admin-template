using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models.Exceptions
{
    /// <summary>
    /// Exception thrown when failing to add an entity to database
    /// </summary>
    public class CreateEntityException : WebApplication1ExceptionBase
    {
        public CreateEntityException()
        {
        }

        public CreateEntityException(Exception ex) : base(ex)
        {
        }

        public CreateEntityException(string message) : base(message)
        {
        }

        public CreateEntityException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CreateEntityException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
