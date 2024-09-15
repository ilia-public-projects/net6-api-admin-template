using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.Admin.User;
using WebApplication1.Services;

namespace WebApplication1.IdentityServices.Services
{
    public interface IUserLoader
    {
        Task<IEnumerable<UserModel>> GetAllUsersAsync();
        Task<UserModel> GetByIdAsync(IOperationContext context, Guid id);
        Task<UserAuthorisationGroupsModel> GetUserAuthorisationGroupsAsync(IOperationContext context, Guid id);
        Task<PagedResult<UserSearchResult>> SearchUsersAsync(UserSearchCriteria criteria);
    }
}
