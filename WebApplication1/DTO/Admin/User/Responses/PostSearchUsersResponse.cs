using AutoMapper;
using WebApplication1.API.DTO.Admin.User.Models;
using WebApplication1.Models;
using WebApplication1.Models.Admin.User;

namespace WebApplication1.API.DTO.Admin.User.Responses
{
    public class PostSearchUsersResponse
    {
        public PostSearchUsersResponse(PagedResult<UserSearchResult> source, IMapper mapper)
        {
            Data = mapper.Map<PagedResult<ResponseUserSearchResult>>(source);
        }

        public PagedResult<ResponseUserSearchResult> Data { get; set; }
    }
}
