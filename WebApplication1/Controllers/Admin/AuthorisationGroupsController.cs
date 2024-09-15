using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.API.DTO.Admin.AuthorisationGroup.Requests;
using WebApplication1.API.DTO.Admin.AuthorisationGroup.Responses;
using WebApplication1.Controllers;
using WebApplication1.DTO;
using WebApplication1.Models.Admin.AuthorisationGroup;
using WebApplication1.Models;
using WebApplication1.Services;
using WebApplication1.RequestFilters;
using WebApplication1.IdentityServices.Services;
using WebApplication1.API.Extensions;

namespace WebApplication1.API.Controllers.Admin
{
    [Route("api/authorisationgroups")]
    [ApiController]
    [Authorize("Admin")]
    public class AuthorisationGroupsController : BaseController
    {
        private readonly IAuthorisationGroupLoader authorisationGroupLoader;
        private readonly IAuthorisationGroupEntryService authorisationGroupEntryService;
        private readonly IMapper mapper;

        public AuthorisationGroupsController(
            IAuthorisationGroupLoader authorisationGroupLoader,
            IAuthorisationGroupEntryService authorisationGroupEntryService,
            IMapper mapper
            )
        {
            this.authorisationGroupLoader = authorisationGroupLoader;
            this.authorisationGroupEntryService = authorisationGroupEntryService;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("search")]
        public async Task<APIResponse<PostSearchAuthorisationGroupsResponse>> Search()
        {
            IOperationContext context = HttpContext.GetOperationContext();
            PagedResult<AuthorisationGroupsSearchResult> result = await authorisationGroupLoader.SearchAsync();
            return new APIResponse<PostSearchAuthorisationGroupsResponse>(new PostSearchAuthorisationGroupsResponse(result, mapper));
        }

        [HttpPost]
        [Route("")]
        public async Task<APIResponse<PostCreateAuthorisationGroupResponse>> Create([FromBody] PostCreateAuthorisationGroupRequest request)
        {
            IOperationContext context = HttpContext.GetOperationContext();
            await authorisationGroupEntryService.CreateAsync(context, request.Name);
            return new APIResponse<PostCreateAuthorisationGroupResponse>(new PostCreateAuthorisationGroupResponse());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<APIResponse<PutUpdateAuthorisationGroupResponse>> Edit([FromRoute] Guid id, [FromBody] PutUpdateAuthorisationGroupRequest request)
        {
            IOperationContext context = HttpContext.GetOperationContext();
            await authorisationGroupEntryService.UpdateAsync(context, id, request.Name);
            return new APIResponse<PutUpdateAuthorisationGroupResponse>(new PutUpdateAuthorisationGroupResponse());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<APIResponse<GetAuthorisationGroupResponse>> GetById([FromRoute] Guid id)
        {
            IOperationContext context = HttpContext.GetOperationContext();
            AuthorisationGroupModel result = await authorisationGroupLoader.GetByIdAsync(context, id);
            return new APIResponse<GetAuthorisationGroupResponse>(new GetAuthorisationGroupResponse(result, mapper));
        }

        [HttpGet]
        [Route("{id}/permissions")]
        public async Task<APIResponse<GetAuthorisationGroupPermissationsReponse>> GetAllPermissions([FromRoute] Guid id)
        {
            IOperationContext context = HttpContext.GetOperationContext();
            AuthorisationGroupPermissionModel result = await authorisationGroupLoader.GetGroupPermissionsAsync(context, id);
            return new APIResponse<GetAuthorisationGroupPermissationsReponse>(new GetAuthorisationGroupPermissationsReponse(result, mapper));
        }

        [HttpPut]
        [Route("{id}/permissions")]
        public async Task<APIResponse<PutUpdateAuthorisationGroupPermissionsResponse>> UpdatePermissions([FromRoute] Guid id, [FromBody] PutUpdateAuthorisationGroupPermissionsRequest request)
        {
            IOperationContext context = HttpContext.GetOperationContext();
            await authorisationGroupEntryService.UpdateGroupPermissionsAsync(context, id, request.SelectedRoleIds);
            return new APIResponse<PutUpdateAuthorisationGroupPermissionsResponse>(new PutUpdateAuthorisationGroupPermissionsResponse());
        }
    }
}
