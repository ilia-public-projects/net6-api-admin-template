using AutoMapper;
using WebApplication1.API.DTO.Admin.AuthorisationGroup.Model;
using WebApplication1.Models.Admin.AuthorisationGroup;
using WebApplication1.Models;

namespace WebApplication1.API.Mapping.Admin
{
    public class AuthorisationGroupProfile : Profile
    {
        public AuthorisationGroupProfile()
        {
            CreateMap<AuthorisationGroupsSearchResult, ResponseAuthorisationGroupSearchResult>();
            CreateMap<PagedResult<AuthorisationGroupsSearchResult>, PagedResult<ResponseAuthorisationGroupSearchResult>>()
                .ForMember(x => x.Results, opt => opt.MapFrom(dest => dest.Results));

            CreateMap<AuthorisationGroupModel, ResponseAuthorisationGroupModel>();

            CreateMap<AuthorisationGroupPermissionModel, ResponseAuthorisationGroupPermission>();
            CreateMap<RoleSelection, ResponseAuthorisationGroupRoleSelection>();
        }
    }
}
