using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models.Admin.AuthorisationGroup
{
    public class AuthorisationGroupPermissionModel
    {
        public string Name { get; set; }
        public List<RoleSelection> Roles { get; set; }
        public AuthorisationGroupPermissionModel()
        {
            Roles = new List<RoleSelection>();
        }
    }
}
