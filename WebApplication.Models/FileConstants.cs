using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public static class FileConstants
    {
        public const string SupportedImageFileContentTypes = "image/jpeg,image/png,image/gif,image/tiff,image/bmp";
        public const string ExcelFileContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        public const string SupportedZipFileContentTypes = "application/x-zip-compressed,application/zip";
        public const long OneMegaByte = 1024 * 1024;
        public const long FiveMegaByte = 1024 * 1024 * 5;
        public const long HundredMegaByte = 1024 * 1024 * 100;
    }
}
