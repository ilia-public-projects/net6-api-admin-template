using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Services
{
    public interface IAzureStorageService
    {
        Task<string> UploadStreamAsync(IOperationContext context, string containerName, Stream file, string contentType);
    }
}
