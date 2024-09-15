using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models.Admin.User
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public string PhotoUriThumbnail { get; set; }
        public string PhotoUriRaw { get; set; }
    }
}
