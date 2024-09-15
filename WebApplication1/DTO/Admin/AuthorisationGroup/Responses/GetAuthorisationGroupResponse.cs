using AutoMapper;
using WebApplication1.API.DTO.Admin.AuthorisationGroup.Model;
using WebApplication1.Models.Admin.AuthorisationGroup;

namespace WebApplication1.API.DTO.Admin.AuthorisationGroup.Responses
{
    public class GetAuthorisationGroupResponse
    {
        public GetAuthorisationGroupResponse(AuthorisationGroupModel source, IMapper mapper)
        {
            Data = mapper.Map<ResponseAuthorisationGroupModel>(source);
        }

        public ResponseAuthorisationGroupModel Data { get; set; }
    }
}
