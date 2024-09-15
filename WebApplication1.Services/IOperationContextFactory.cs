using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Services
{
    public interface IOperationContextFactory
    {
        IOperationContext CreateBackgrounOperationContext(Guid? ownerId);
        IOperationContext CreateOperationContext(HttpRequest request);
    }
}
