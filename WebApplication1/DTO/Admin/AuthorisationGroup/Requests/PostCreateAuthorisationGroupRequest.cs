using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.API.DTO.Admin.AuthorisationGroup.Requests
{
    public class PostCreateAuthorisationGroupRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
