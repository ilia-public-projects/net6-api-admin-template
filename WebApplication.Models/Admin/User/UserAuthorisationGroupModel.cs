using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models.Admin.User
{
    public class UserAuthorisationGroupModel
    {
        public Guid GroupId { get; set; }
        public string GroupName { get; set; }
        public bool IsSelected { get; set; }
    }
}
