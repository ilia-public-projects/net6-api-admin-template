using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.API.DTO.Admin.AuthorisationGroup.Model
{
    public class ResponseAuthorisationGroupPermission
    {
        public string Name { get; set; }
        public List<ResponseAuthorisationGroupRoleSelection> Roles { get; set; }
        public ResponseAuthorisationGroupPermission()
        {
            Roles = new List<ResponseAuthorisationGroupRoleSelection>();
        }
    }

    public class ResponseAuthorisationGroupRoleSelection
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RoleCount { get; set; }
        public bool Selected { get; set; }
        public bool HasParents { get; set; }
        public List<ResponseAuthorisationGroupRoleSelection> Children { get; set; }
        public ResponseAuthorisationGroupRoleSelection()
        {
            Children = new List<ResponseAuthorisationGroupRoleSelection>();
        }
    }
}
