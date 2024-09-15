using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.EntityFramework.Identity.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<UserRepository> logger;

        public UserRepository(
                ApplicationDbContext dbContext,
                ILogger<UserRepository> logger
            )
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }
    }
}
