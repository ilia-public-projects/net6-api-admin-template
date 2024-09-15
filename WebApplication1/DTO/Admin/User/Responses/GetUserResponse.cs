using AutoMapper;
using WebApplication1.API.DTO.Admin.User.Models;
using WebApplication1.Models.Admin.User;

namespace WebApplication1.API.DTO.Admin.User.Responses
{
    public class GetUserResponse
    {
        public ResponseUserModel Data { get; set; }

        public GetUserResponse(UserModel userModel, IMapper mapper)
        {
            Data = mapper.Map<ResponseUserModel>(userModel);
        }
    }
}
