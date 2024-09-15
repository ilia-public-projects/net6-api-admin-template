using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models.Admin.AuthorisationGroup
{
    public class RoleSelection
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RoleCount { get; set; }
        public bool Selected { get; set; }
        public bool HasParents { get; set; }
        public List<RoleSelection> Children { get; set; }
        public RoleSelection()
        {
            Children = new List<RoleSelection>();
        }
    }
}
