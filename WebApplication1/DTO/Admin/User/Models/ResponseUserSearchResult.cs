using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.API.DTO.Admin.User.Models
{
    public class ResponseUserSearchResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string ThumbnailUri { get; set; }
    }
}
