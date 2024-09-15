using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.EntityFramework.Identity
{
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(Name), IsUnique = true)]
    [Index(nameof(IsActive))]
    public class User
    {
        public User()
        {
            Roles = new List<Role>();
            Groups = new List<IdentityGroup>();
        }

        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public bool IsActive { get; set; }
        /// <summary>
        /// Small photo for display in lists.
        /// </summary>
        public string PhotoUriThumbnail { get; set; }
        /// <summary>
        /// Original photo.
        /// </summary>
        public string PhotoUriRaw { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<IdentityGroup> Groups { get; set; }
    }
}
