using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.DTO;

namespace WebApplication1.API.RequestFilters
{
    public static class InvalidModelStateProcessor
    {
        /// <summary></summary>
        public static BadRequestObjectResult ReturnBadRequestWithModelStateErrors(ActionContext actionContext)
        {
            List<string> modelStateErrors = new List<string>();
            foreach (KeyValuePair<string, Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateEntry> state in actionContext.ModelState)
            {
                foreach (Microsoft.AspNetCore.Mvc.ModelBinding.ModelError error in state.Value.Errors)
                {
                    if (!modelStateErrors.Any(x => x == error.ErrorMessage))
                    {
                        modelStateErrors.Add(string.IsNullOrEmpty(error.ErrorMessage) ? error.Exception.Message : error.ErrorMessage);
                    }
                }
            }
            APIResponse<string> apiResponse = new APIResponse<string>($"Invalid parameters", modelStateErrors.ToArray());

            return new BadRequestObjectResult(apiResponse);
        }
    }
}
