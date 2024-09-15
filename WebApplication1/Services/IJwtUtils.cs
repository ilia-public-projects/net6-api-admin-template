
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DTO.Authentication;

namespace WebApplication1.Services
{
    public interface IJwtUtils
    {
        string GenerateToken(UserAuthorisationModel user);
        UserAuthorisationModel? ValidateToken(string token);
    }
}
