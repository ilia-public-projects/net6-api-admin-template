using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Services.Utils;
using WebApplication1.Services;
using WebApplication1.Models.Exceptions;

namespace WebApplication1.Common
{
    public class RestService : IRestService
    {
        private readonly ILogger<RestService> logger;

        public RestService(ILogger<RestService> logger)
        {
            this.logger = logger;
        }

        public async Task<T> PostAsync<T>(IOperationContext context, string url, object data, List<KeyValuePairModel> headers)
        {
            T result = default;
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    if (headers?.Any() ?? false)
                    {
                        foreach (KeyValuePairModel header in headers)
                        {
                            httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                        }
                    }

                    HttpResponseMessage response = await httpClient.PostAsJsonAsync(url, data);
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<T>(content);
                    }
                    else
                    {
                        string errorMessage = await response.Content.ReadAsStringAsync();
                        ErrorUtils.LogAndThrowException(context, logger, $"REST request ({url}) did not return status code 200. Status code: ({response.StatusCode}), message: {errorMessage}", () => throw new RestBadRequestException());
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorUtils.LogAndThrowException(context, logger, $"An exception occured while sending REST request to ({url}), message: {ex.Message}", () => throw new RestCommunicationException());
            }

            return result;
        }

        public async Task<T> GetAsync<T>(IOperationContext context, string url, List<KeyValuePairModel> headers)
        {
            T result = default;
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    if (headers?.Any() ?? false)
                    {
                        foreach (KeyValuePairModel header in headers)
                        {
                            httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                        }
                    }

                    HttpResponseMessage response = await httpClient.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<T>(content);
                    }
                    else
                    {
                        string errorMessage = await response.Content.ReadAsStringAsync();
                        ErrorUtils.LogAndThrowException(context, logger, $"REST request ({url}) did not return status code 200. Status code: ({response.StatusCode}), message: {errorMessage}", () => throw new RestBadRequestException());
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorUtils.LogAndThrowException(context, logger, $"An exception occured while sending REST request to ({url}), message: {ex.Message}", () => throw new RestCommunicationException());
            }

            return result;
        }
    }
}
