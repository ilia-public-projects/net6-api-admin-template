using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class UploadDocumentParams
    {
        public string FileName { get; set; }
        public Stream File { get; set; }
        public string ContentType { get; set; }
    }
}
