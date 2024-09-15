using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models.Common
{
    public class AppSettings
    {
        /// <summary>
        /// This is the secret key used to encrypt and decrypt JWT tokens.
        /// </summary>
        public string Secret { get; set; }
        /// <summary>
        /// Identifies the Azure Storage Account to use for file storage.
        /// </summary>
        public string AzureFileStorageConnectionString { get; set; }
        /// <summary>
        /// Identifies the Azure Service Bus to use for messaging.
        /// </summary>
        public string AzureServiceBusConnectionString { get; set; }
    }
}
