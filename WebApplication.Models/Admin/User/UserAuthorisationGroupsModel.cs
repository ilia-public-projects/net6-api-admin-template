using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models.Admin.User
{
    public class UserAuthorisationGroupsModel
    {
        public string Name { get; set; }
        public List<UserAuthorisationGroupModel> Groups { get; set; }

        public UserAuthorisationGroupsModel()
        {
            Groups = new List<UserAuthorisationGroupModel>();
        }
    }
}
