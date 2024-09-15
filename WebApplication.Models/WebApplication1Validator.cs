using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models.Exceptions;

namespace WebApplication1.Models
{
    public class WebApplication1Validator
    {
        public WebApplication1Validator()
        {
            Errors = new List<string>();
        }

        public List<string> Errors { get; set; }

        public void Validate(string message = "Validation error")
        {
            if (Errors.Any())
            {
                throw new WebApplication1ValidationException(message, Errors);
            }
        }

        public void WriteError(string message)
        {
            Errors.Add(message);
        }

        public void WriteError(string message, int? lineNo)
        {
            if (lineNo.HasValue)
            {
                WriteError(message);
            }
            else
            {
                WriteLineError(lineNo.Value, message);
            }
        }

        /// <summary>
        /// Add validation error and throw validation exception
        /// </summary>
        /// <param name="message">Validation error</param>
        public void WriteErrorAndValidate(string message)
        {
            WriteError(message);
            Validate();
        }

        public void WriteLineError(int lineNo, string message)
        {
            Errors.Add($"Line no ({lineNo}), message: {message}");
        }
    }
}
