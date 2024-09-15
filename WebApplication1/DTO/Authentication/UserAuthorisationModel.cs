using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.DTO.Authentication
{
    public class UserAuthorisationModel
    {
        public Guid Id { get; set; }
        /// <summary></summary>
        public string Name { get; set; }
        /// <summary></summary>
        public string Email { get; set; }
        /// <summary></summary>
        public string[] Roles { get; set; }
        public string PhotoUri { get; set; }
    }
}
