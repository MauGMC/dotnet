using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Presentacion.Validaciones
{
    public class AllowedExtensionsAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly string[] _allowedExtensions;
        public AllowedExtensionsAttribute(string[] allowedExtensions)
        {
            _allowedExtensions = allowedExtensions;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!_allowedExtensions.Contains(extension))
                {
                    return new ValidationResult(ErrorMessage ?? $"The file extension is not allowed. Allowed extensions are: {string.Join(", ", _allowedExtensions)}.");
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
            MergeAttribute(context.Attributes, "data-val-allowedextensions", ErrorMessage ?? $"The file extension is not allowed. Allowed extensions are: {string.Join(", ", _allowedExtensions)}.");
            MergeAttribute(context.Attributes, "data-val-allowedextensions-extensions", string.Join(", ", _allowedExtensions));
        }
    }
}
