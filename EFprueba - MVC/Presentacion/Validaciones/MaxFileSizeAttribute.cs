using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Presentacion.Validaciones
{
    public class MaxFileSizeAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly int _maxFileSizeInBytes;
        public MaxFileSizeAttribute(int maxFileSizeInMB)
        {
            _maxFileSizeInBytes = maxFileSizeInMB*1024*1024;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                if (file.Length > _maxFileSizeInBytes)
                {
                    return new ValidationResult(ErrorMessage ?? $"The file size cannot exceed {_maxFileSizeInBytes / 1024 / 1024} MB.");
                }
            }
            return ValidationResult.Success;
        }
        public bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key)) return false;
            attributes.Add(key, value);
            return true;
        }
        public void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-maxfilesize", ErrorMessage ?? $"The file size cannot exceed {_maxFileSizeInBytes / 1024 / 1024} MB.");
            MergeAttribute(context.Attributes, "data-val-maxfilesize-max", _maxFileSizeInBytes.ToString());
        }
    }
}
