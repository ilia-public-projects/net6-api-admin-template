using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApplication1.Models.Common;
using WebApplication1.Models.Exceptions;
using WebApplication1.Services;
using WebApplication1.Services.Utils;

namespace WebApplication1.Common
{
    public class AzureServiceBusMessageSender : IAzureServiceBusMessageSender
    {
        private readonly ILogger<AzureServiceBusMessageSender> logger;
        private readonly IOptions<AppSettings> options;

        public AzureServiceBusMessageSender(
                ILogger<AzureServiceBusMessageSender> logger,
                IOptions<AppSettings> options
            )
        {
            this.logger = logger;
            this.options = options;
        }

        public async Task SendBusMessageAsync(IOperationContext context, string message, string topicName)
        {
            string connectionString = options.Value.AzureServiceBusConnectionString;

            if (string.IsNullOrEmpty(connectionString))
            {
                return;

                // TODO: once configured in Azure uncomment code and throw exception
                //ErrorUtils.LogAndThrowException(context, logger, $"Azure Service Bus connection string is not configured.", 
                //    () => throw new InvalidOperationException("Azure Service Bus connection string is not configured."));
            }

            ServiceBusClient client = new ServiceBusClient(connectionString);
            ServiceBusSender sender = client.CreateSender(topicName);


            try
            {
                ServiceBusMessage sbMessage = new ServiceBusMessage(message);
                await sender.SendMessageAsync(sbMessage);
            }
            catch (Exception ex)
            {
                ErrorUtils.LogAndThrowException(context, logger, $"Failed to send service bus message ({message}) for topic ({topicName}), error message: {ex.Message}"
                    , () => throw new AzureServiceBusMessageException(ex.Message, ex.InnerException));
            }
        }
    }
}
