using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.EntityFramework.Identity
{
    [Index(nameof(Name), IsUnique = true)]
    public class IdentityGroup
    {
        public IdentityGroup()
        {
            Roles = new List<Role>();
            Users = new List<User>();
        }

        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
