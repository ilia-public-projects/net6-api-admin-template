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
    public class Role
    {
        public Role()
        {
            Users = new List<User>();
            Groups = new List<IdentityGroup>();
        }

        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(256)]
        public string Name { get; set; }
        [Required]
        [StringLength(256)]
        public string Description { get; set; }
        /// <summary>
        /// Indicates the name of the parent role. Optional.
        /// </summary>
        public string ParentName { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<IdentityGroup> Groups { get; set; }
    }
}
