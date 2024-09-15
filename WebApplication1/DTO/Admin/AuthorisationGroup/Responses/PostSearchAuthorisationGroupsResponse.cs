using AutoMapper;
using WebApplication1.API.DTO.Admin.AuthorisationGroup.Model;
using WebApplication1.Models;
using WebApplication1.Models.Admin.AuthorisationGroup;

namespace WebApplication1.API.DTO.Admin.AuthorisationGroup.Responses
{
    public class PostSearchAuthorisationGroupsResponse
    {
        public PagedResult<ResponseAuthorisationGroupSearchResult> Data { get; set; }

        public PostSearchAuthorisationGroupsResponse(PagedResult<AuthorisationGroupsSearchResult> source, IMapper mapper)
        {
            Data = mapper.Map<PagedResult<ResponseAuthorisationGroupSearchResult>>(source);
        }
    }
}
