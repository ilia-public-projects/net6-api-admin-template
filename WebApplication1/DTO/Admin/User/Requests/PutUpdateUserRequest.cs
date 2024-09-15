using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.API.DTO.Admin.User.Requests
{
    public class PutUpdateUserRequest
    {
        [Required]
        [StringLength(100, ErrorMessage = "Full name cannot be longer than 100 characters")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
