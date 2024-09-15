using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.API.DTO.Admin.User.Models
{
    public class ResponseUserAuthorisationGroupsModel
    {
        public string Name { get; set; }
        public IEnumerable<ResponseUserAuthorisationGroupModel> Groups { get; set; }
    }

    public class ResponseUserAuthorisationGroupModel
    {
        public Guid GroupId { get; set; }
        public string GroupName { get; set; }
        public bool IsSelected { get; set; }
    }
}
