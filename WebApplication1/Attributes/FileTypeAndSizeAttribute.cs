using System.ComponentModel.DataAnnotations;

namespace WebApplication1.API.Attributes
{
    public class FileTypeAndSizeAttribute : ValidationAttribute
    {
        private readonly string[] allowedFileTypes;
        private readonly long maxFileSize;

        public FileTypeAndSizeAttribute(string fileTypes, long maxFileSize)
        {
            allowedFileTypes = fileTypes.Split(',');
            this.maxFileSize = maxFileSize;
        }

        public FileTypeAndSizeAttribute(long maxFileSize)
        {
            this.maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            if (value is IFormFile file)
            {
                return ValidateSingleFile(file);
            }
            else if (value is List<IFormFile> files)
            {
                foreach (IFormFile f in files)
                {
                    ValidationResult validationResult = ValidateSingleFile(f);
                    if (validationResult != ValidationResult.Success)
                    {
                        return validationResult;
                    }
                }
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Invalid file type or size.");
            }
        }

        private ValidationResult ValidateSingleFile(IFormFile file)
        {
            if (allowedFileTypes != null && allowedFileTypes.Any())
            {
                if (!allowedFileTypes.Contains(file.ContentType))
                {
                    return new ValidationResult(ErrorMessage ?? $"File type ({file.ContentType}) is not allowed (file: {file.FileName}).");
                }
            }

            if (file.Length >= maxFileSize)
            {
                double mb = Math.Round((double)maxFileSize / 1048576, 2);
                return new ValidationResult(ErrorMessage ?? $"File size note allowed (file: {file.FileName}). Max file size allowed: {mb}MB");
            }

            return ValidationResult.Success;
        }
    }
}
