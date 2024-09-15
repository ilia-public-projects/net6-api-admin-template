using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.EntityFramework;
using WebApplication1.Models.Common;

namespace WebApplication1.Services
{
    public interface IDocumentNoSchemeService
    {
        Task<string> GenerateDocumentNoAsync(IOperationContext context, ApplicationDbContext dbContext, DocumentNoSchemeType schemeType);
    }
}
