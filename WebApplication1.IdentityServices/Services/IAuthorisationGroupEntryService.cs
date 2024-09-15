using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Services;

namespace WebApplication1.IdentityServices.Services
{
    public interface IAuthorisationGroupEntryService
    {
        Task CreateAsync(IOperationContext context, string name);
        Task UpdateAsync(IOperationContext context, Guid id, string name);
        Task UpdateGroupPermissionsAsync(IOperationContext context, Guid groupId, List<Guid> selectedRoleIds);
    }
}
