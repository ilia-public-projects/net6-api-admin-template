using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.DTO.Authentication
{
    public class PostLoginResponse
    {
        public PostLoginResponse(string token)
        {
            Token = token;
        }

        public string Token { get; set; }
    }
}
