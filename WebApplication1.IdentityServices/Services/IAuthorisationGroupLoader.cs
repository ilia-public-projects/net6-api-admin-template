using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.Admin.AuthorisationGroup;
using WebApplication1.Services;

namespace WebApplication1.IdentityServices.Services
{
    public interface IAuthorisationGroupLoader
    {
        Task<AuthorisationGroupModel> GetByIdAsync(IOperationContext context, Guid id);
        Task<AuthorisationGroupPermissionModel> GetGroupPermissionsAsync(IOperationContext context, Guid id);
        Task<PagedResult<AuthorisationGroupsSearchResult>> SearchAsync();
    }
}
