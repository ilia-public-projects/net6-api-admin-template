using AutoMapper;
using WebApplication1.API.DTO.Admin.User.Models;
using WebApplication1.Models.Admin.User;

namespace WebApplication1.API.DTO.Admin.User.Responses
{
    public class GetAllUsersResponse
    {
        public GetAllUsersResponse(IEnumerable<UserModel> source, IMapper mapper)
        {
            Data = mapper.Map<IEnumerable<ResponseUserModel>>(source);
        }

        public IEnumerable<ResponseUserModel> Data { get; set; }
    }
}
