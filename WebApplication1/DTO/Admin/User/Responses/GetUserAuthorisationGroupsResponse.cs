using AutoMapper;
using WebApplication1.API.DTO.Admin.User.Models;
using WebApplication1.Models.Admin.User;

namespace WebApplication1.API.DTO.Admin.User.Responses
{
    public class GetUserAuthorisationGroupsResponse
    {
        public ResponseUserAuthorisationGroupsModel Data { get; set; }

        public GetUserAuthorisationGroupsResponse(UserAuthorisationGroupsModel model, IMapper mapper)
        {
            Data = mapper.Map<ResponseUserAuthorisationGroupsModel>(model);
        }
    }
}
