using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.API.DTO.Admin.AuthorisationGroup.Requests
{
    public class PutUpdateAuthorisationGroupPermissionsRequest
    {
        public List<Guid> SelectedRoleIds { get; set; }
    }
}
