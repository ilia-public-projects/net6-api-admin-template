using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.API.DTO.Admin.User.Requests;
using WebApplication1.API.DTO.Admin.User.Responses;
using WebApplication1.Controllers;
using WebApplication1.DTO;
using WebApplication1.Models.Admin.User;
using WebApplication1.Models;
using WebApplication1.Services;
using WebApplication1.IdentityServices.Services;
using WebApplication1.RequestFilters;
using WebApplication1.API.Extensions;

namespace WebApplication1.API.Controllers.Admin
{
    [Route("api/user")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserLoader userLoader;
        private readonly IUserEntryService userEntryService;
        private readonly IMapper mapper;

        public UserController(
            IUserLoader userLoader,
            IUserEntryService userEntryService,
            IMapper mapper
            )
        {
            this.userLoader = userLoader;
            this.userEntryService = userEntryService;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("search")]
        [Authorize("Admin")]
        public async Task<APIResponse<PostSearchUsersResponse>> Search([FromBody] PostSearchUsersRequest request)
        {
            IOperationContext context = HttpContext.GetOperationContext();
            PagedResult<UserSearchResult> result = await userLoader.SearchUsersAsync(mapper.Map<UserSearchCriteria>(request.Data));
            return new APIResponse<PostSearchUsersResponse>(new PostSearchUsersResponse(result, mapper));
        }

        [HttpPost]
        [Route("")]
        [Authorize("Admin")]
        public async Task<APIResponse<PostCreateUserResponse>> Create([FromBody] PostCreateUserRequest request)
        {
            IOperationContext context = HttpContext.GetOperationContext();
            await userEntryService.CreateUserAsync(context, mapper.Map<UserCreateModel>(request));
            return new APIResponse<PostCreateUserResponse>(new PostCreateUserResponse());
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize("Admin")]
        public async Task<APIResponse<PutUpdateUserResponse>> Edit([FromRoute] Guid id, [FromBody] PutUpdateUserRequest request)
        {
            IOperationContext context = HttpContext.GetOperationContext();
            await userEntryService.UpdateUserAsync(context, id, mapper.Map<UserUpdateModel>(request));
            return new APIResponse<PutUpdateUserResponse>(new PutUpdateUserResponse());
        }


        [HttpGet]
        [Route("{id}")]
        [Authorize("Admin")]
        public async Task<APIResponse<GetUserResponse>> GetById([FromRoute] Guid id)
        {
            IOperationContext context = HttpContext.GetOperationContext();
            UserModel result = await userLoader.GetByIdAsync(context, id);
            return new APIResponse<GetUserResponse>(new GetUserResponse(result, mapper));
        }

        [HttpPut]
        [Route("{id}/changepassword")]
        [Route("changepassword")]
        [Authorize]
        public async Task<APIResponse<PutChangePasswordResponse>> ChangePassword([FromRoute] Guid? id, [FromBody] PutChangePasswordRequest request)
        {
            IOperationContext context = HttpContext.GetOperationContext();
            await userEntryService.ChangePasswordAsync(context, id, mapper.Map<ChangePasswordModel>(request));
            return new APIResponse<PutChangePasswordResponse>(new PutChangePasswordResponse());
        }

        [HttpGet]
        [Route("{id}/authorisationgroups")]
        [Authorize("Admin")]
        public async Task<APIResponse<GetUserAuthorisationGroupsResponse>> GetUserAuthorisationGroups([FromRoute] Guid id)
        {
            IOperationContext context = HttpContext.GetOperationContext();
            UserAuthorisationGroupsModel result = await userLoader.GetUserAuthorisationGroupsAsync(context, id);
            return new APIResponse<GetUserAuthorisationGroupsResponse>(new GetUserAuthorisationGroupsResponse(result, mapper));
        }

        [HttpPut]
        [Route("{id}/authorisationgroups")]
        [Authorize("Admin")]
        public async Task<APIResponse<PutUpdateUserAuthorisationGroupsResponse>> UpdateUserAuthorisationGroups([FromRoute] Guid id, [FromBody] PutUpdateUserAuthorisationGroupsRequest request)
        {
            IOperationContext context = HttpContext.GetOperationContext();
            await userEntryService.UpdateUserAuthorisationGroupsAsync(context, id, request.SelectedGroupIds);
            return new APIResponse<PutUpdateUserAuthorisationGroupsResponse>(new PutUpdateUserAuthorisationGroupsResponse());
        }

        [HttpGet]
        [Route("getall")]
        [Authorize]
        public async Task<APIResponse<GetAllUsersResponse>> GetAllUsers()
        {
            IOperationContext context = HttpContext.GetOperationContext();
            IEnumerable<UserModel> result = await userLoader.GetAllUsersAsync();
            return new APIResponse<GetAllUsersResponse>(new GetAllUsersResponse(result, mapper));
        }

        [HttpPut]
        [Route("{userId}/photo")]
        [Route("photo")]
        [Authorize("")]
        public async Task<APIResponse<PutChangeUserPhotoResponse>> ChangeUserPhoto([FromRoute] Guid? userId, [FromForm] PutChangeUserPhotoRequest request)
        {
            IOperationContext context = HttpContext.GetOperationContext();
            string photoUri = await userEntryService.ChangePhotoAsync(context, userId, request.Photo);
            return new APIResponse<PutChangeUserPhotoResponse>(new PutChangeUserPhotoResponse { PhotoUri = photoUri });
        }
    }
}
