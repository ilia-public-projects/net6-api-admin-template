using AutoMapper;
using WebApplication1.API.DTO.Admin.User.Models;
using WebApplication1.API.DTO.Admin.User.Requests;
using WebApplication1.Models.Admin.User;
using WebApplication1.Models;

namespace WebApplication1.API.Mapping.Admin
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<PostCreateUserRequest, UserCreateModel>();
            CreateMap<PutUpdateUserRequest, UserUpdateModel>();
            CreateMap<UserModel, ResponseUserModel>();

            CreateMap<RequestUserSearchCriteria, UserSearchCriteria>();
            CreateMap<PagedResult<UserSearchResult>, PagedResult<ResponseUserSearchResult>>();
            CreateMap<UserSearchResult, ResponseUserSearchResult>();

            CreateMap<UserAuthorisationGroupsModel, ResponseUserAuthorisationGroupsModel>();
            CreateMap<UserAuthorisationGroupModel, ResponseUserAuthorisationGroupModel>();

            CreateMap<PutChangePasswordRequest, ChangePasswordModel>();
        }
    }
}
