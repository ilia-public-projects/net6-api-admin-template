using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Services.Utils
{
    public static class ZipFileUtils
    {
        public static Dictionary<string, List<ZipArchiveEntry>> BuildFolderNameFileMap(ZipArchive archive)
        {
            List<string> folderNames = GetFolderNames(archive);
            Dictionary<string, List<ZipArchiveEntry>> folderDocuments = new Dictionary<string, List<ZipArchiveEntry>>();
            foreach (string folderName in folderNames)
            {
                folderDocuments.Add(folderName, new List<ZipArchiveEntry>());
            }

            // build map of folder names to zip archive files
            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                if (entry.FullName.Split("/").Any())
                {
                    folderDocuments[entry.FullName.Split('/')[0]].Add(entry);
                }
            }

            return folderDocuments;
        }

        public static List<UploadDocumentParams> BuildUploadParamsFor(KeyValuePair<string, List<ZipArchiveEntry>> map)
        {
            List<UploadDocumentParams> documentParams = new List<UploadDocumentParams>();
            foreach (ZipArchiveEntry subEntry in map.Value)
            {
                FileExtensionContentTypeProvider provider = new FileExtensionContentTypeProvider();
                string contentType;
                if (!provider.TryGetContentType(subEntry.Name, out contentType))
                {
                    contentType = "application/octet-stream";
                }

                documentParams.Add(new UploadDocumentParams
                {
                    ContentType = contentType,
                    File = subEntry.Open(),
                    FileName = subEntry.Name
                });
            }
            return documentParams;
        }

        private static List<string> GetFolderNames(ZipArchive archive)
        {
            return archive.Entries.Where(x => x.FullName.Split('/').Any()).Select(x => x.FullName.Split('/')[0]).Distinct().ToList();
        }
    }
}
