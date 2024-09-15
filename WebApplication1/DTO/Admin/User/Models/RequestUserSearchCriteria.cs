using WebApplication1.Models;

namespace WebApplication1.API.DTO.Admin.User.Models
{
    public class RequestUserSearchCriteria : PageQuery
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool? IncludeInactive { get; set; }
    }
}
