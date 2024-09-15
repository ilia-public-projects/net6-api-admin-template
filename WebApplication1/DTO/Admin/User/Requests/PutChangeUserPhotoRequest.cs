using WebApplication1.API.Attributes;
using WebApplication1.Models;

namespace WebApplication1.API.DTO.Admin.User.Requests
{
    public class PutChangeUserPhotoRequest
    {
        [FileTypeAndSize(FileConstants.SupportedImageFileContentTypes, FileConstants.OneMegaByte)]
        public IFormFile Photo { get; set; }
    }
}
