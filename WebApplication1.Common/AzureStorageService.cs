using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using WebApplication1.Models.Common;
using WebApplication1.Models.Exceptions;
using WebApplication1.Services;
using WebApplication1.Services.Utils;

namespace WebApplication1.Common
{
    public class AzureStorageService : IAzureStorageService
    {
        private readonly ILogger<AzureStorageService> logger;
        private readonly IOptions<AppSettings> options;
        private Dictionary<string, CloudBlobContainer> containers = new Dictionary<string, CloudBlobContainer>();

        public AzureStorageService(
                ILogger<AzureStorageService> logger,
                IOptions<AppSettings> options
            )
        {
            this.logger = logger;
            this.options = options;
        }

        public async Task<string> UploadStreamAsync(IOperationContext context, string containerName, Stream file, string contentType)
        {
            try
            {
                string connectionString = options.Value.AzureFileStorageConnectionString;
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                CloudBlobContainer container = null;
                if (containers.ContainsKey(containerName))
                {
                    container = containers[containerName];
                }
                else
                {
                    container = blobClient.GetContainerReference(containerName);
                    containers.Add(containerName, container);
                }

                bool exists = await container.CreateIfNotExistsAsync();
                if (!exists)
                {
                    await container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
                }

                BlobContainerPermissions permissios = await container.GetPermissionsAsync();
                if (permissios.PublicAccess != BlobContainerPublicAccessType.Blob)
                {
                    permissios.PublicAccess = BlobContainerPublicAccessType.Blob;
                    await container.SetPermissionsAsync(permissios);
                }


                CloudBlockBlob blob = container.GetBlockBlobReference($"{Guid.NewGuid()}");
                await blob.UploadFromStreamAsync(file);

                blob.Properties.ContentType = contentType;
                await blob.SetPropertiesAsync();

                return blob.Uri.ToString();

            }
            catch (StorageException ex)
            {
                ErrorUtils.LogAndThrowException(context, logger,
                    $"Error uploading file to azure storage, message: {ex.Message}", () => { throw new AzureStorageException(ex.Message, ex.InnerException); });
            }
            catch (Exception ex)
            {
                ErrorUtils.LogAndThrowException(context, logger,
                    $"An an unknown error occured while uploading file to azure storage, message: {ex.Message}", () => { throw new AzureStorageException(ex.Message, ex.InnerException); });
            }

            return null;

        }
    }
}
