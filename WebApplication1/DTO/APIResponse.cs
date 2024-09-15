using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.DTO
{
    public class APIResponse<T>
    {
        public APIResponse(T response)
        {
            Response = response;
            Success = true;
        }

        public APIResponse(string errorCode, string[] errorMessages)
        {
            ErrorCode = errorCode;
            ErrorMessages = errorMessages;
            Success = false;
        }

        public APIResponse(T response, string errorCode, string[] errorMessages)
        {
            Response = response;
            ErrorCode = errorCode;
            ErrorMessages = errorMessages;
            Success = true;
        }

        public bool Success { get; private set; }
        public string ErrorCode { get; private set; }
        public string[] ErrorMessages { get; private set; }
        public T Response { get; private set; }
    }
}
