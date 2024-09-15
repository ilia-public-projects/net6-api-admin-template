using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.EntityFramework;
using WebApplication1.EntityFramework.Common;
using WebApplication1.Models.Common;
using WebApplication1.Services;

namespace WebApplication1.Common
{
    public class DocumentNoSchemeService : IDocumentNoSchemeService
    {
        public async Task<string> GenerateDocumentNoAsync(IOperationContext context, ApplicationDbContext dbContext, DocumentNoSchemeType schemeType)
        {
            await Task.FromResult(0);
            throw new NotImplementedException();

            //long lastDocumentNo = 0;
            //DocumentNoScheme lastScheme = await dbContext.DocumentNoSchemes.FirstOrDefaultAsync(x => x.SchemeType == schemeType);
            //if (lastScheme != null)
            //{
            //    lastDocumentNo = lastScheme.LastDocumentNo;
            //}
            //else
            //{
            //    lastScheme = new DocumentNoScheme
            //    {
            //        SchemeType = schemeType
            //    };

            //    dbContext.DocumentNoSchemes.Add(lastScheme);
            //}

            //long newDocumentNo = lastDocumentNo + 1;
            //lastScheme.LastDocumentNo = newDocumentNo;

            //return $"{newDocumentNo:D7}";
        }
    }
}
