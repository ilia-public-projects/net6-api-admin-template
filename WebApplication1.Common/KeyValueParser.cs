using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebApplication1.Models;
using WebApplication1.Models.Exceptions;
using WebApplication1.Services;
using WebApplication1.Services.Utils;

namespace WebApplication1.Common
{
    public class KeyValueParser : IKeyValueParser
    {
        private readonly ILogger<KeyValueParser> logger;

        public KeyValueParser(
                ILogger<KeyValueParser> logger
            )
        {
            this.logger = logger;
        }

        public string GetSettingValueOrDefault(List<KeyValuePairModel> settings, string key)
        {
            if (settings == null)
            {
                return null;
            }

            KeyValuePairModel setting = settings.FirstOrDefault(x => x.Key == key);
            return setting?.Value;
        }

        public T GetJsonValue<T>(IOperationContext context, List<KeyValuePairModel> settings, string key)
        {
            T result = default(T);
            string value = GetSettingValueOrDefault(settings, key);
            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    result = JsonConvert.DeserializeObject<T>(value);
                }
                catch (JsonException ex)
                {
                    ErrorUtils.LogAndThrowException(context, logger, $"Failed to deserialize json ({value})",
                        () => throw new WebApplication1JsonException(ex.Message, ex.InnerException));
                }
            }

            return result;
        }
    }
}
