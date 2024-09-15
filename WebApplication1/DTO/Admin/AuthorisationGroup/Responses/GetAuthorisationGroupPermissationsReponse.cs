using AutoMapper;
using WebApplication1.API.DTO.Admin.AuthorisationGroup.Model;
using WebApplication1.Models.Admin.AuthorisationGroup;

namespace WebApplication1.API.DTO.Admin.AuthorisationGroup.Responses
{
    public class GetAuthorisationGroupPermissationsReponse
    {
        public ResponseAuthorisationGroupPermission Data { get; set; }

        public GetAuthorisationGroupPermissationsReponse(AuthorisationGroupPermissionModel source, IMapper mapper)
        {
            Data = mapper.Map<ResponseAuthorisationGroupPermission>(source);
        }
    }
}
