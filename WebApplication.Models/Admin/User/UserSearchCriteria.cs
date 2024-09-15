﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models.Admin.User
{
    public class UserSearchCriteria : PageQuery
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool? IncludeInactive { get; set; }
    }
}
