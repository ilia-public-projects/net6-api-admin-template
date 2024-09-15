﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.IdentityServices.Services
{
    public interface IIdentityRoleProvider
    {
        List<WebApplication1.EntityFramework.Identity.Role> GetApplicationRoles();
    }
}
