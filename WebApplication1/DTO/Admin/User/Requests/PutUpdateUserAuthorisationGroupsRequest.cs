using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.API.DTO.Admin.User.Requests
{
    public class PutUpdateUserAuthorisationGroupsRequest
    {
        [Required]
        public List<Guid> SelectedGroupIds { get; set; }
    }
}
