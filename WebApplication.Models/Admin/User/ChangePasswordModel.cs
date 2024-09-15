using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models.Admin.User
{
    public class ChangePasswordModel
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
