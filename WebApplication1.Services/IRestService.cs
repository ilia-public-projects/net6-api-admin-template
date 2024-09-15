using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IRestService
    {
        Task<T> GetAsync<T>(IOperationContext context, string url, List<KeyValuePairModel> headers);
        Task<T> PostAsync<T>(IOperationContext context, string url, object data, List<KeyValuePairModel> headers);
    }
}
