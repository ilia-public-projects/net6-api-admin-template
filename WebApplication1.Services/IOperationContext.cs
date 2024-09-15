using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Services
{
    public interface IOperationContext
    {
        /// <summary>
        /// Each operation has a unique operation ID, which can be obtained by getting this property. 
        /// Use this in logging when the message is specific to the operation.
        /// </summary>
        string OperationId { get; }

        /// <summary>
        /// Raw token value, sent as part of the headers to the api
        /// </summary>
        string Token { get; }

        /// <summary>
        /// User id from the token
        /// </summary>
        Guid UserId { get; }

        List<string> Roles { get; }
    }
}
