using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models.Exceptions
{
    /// <summary>
    /// Exception thrown by azure when performing azure blob storage operations
    /// </summary>
    public class AzureStorageException : WebApplication1ExceptionBase
    {
        public AzureStorageException()
        {
        }

        public AzureStorageException(Exception ex) : base(ex)
        {
        }

        public AzureStorageException(string message) : base(message)
        {
        }

        public AzureStorageException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AzureStorageException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
