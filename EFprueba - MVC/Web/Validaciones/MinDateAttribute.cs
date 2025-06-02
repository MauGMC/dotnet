using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Web.Validaciones
{
    public class MinDateAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly DateTime _minDate;
        public MinDateAttribute(string minDate)
        {
            _minDate = DateTime.Parse(minDate);
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is DateTime fecha)
            {
                if (fecha < _minDate)
                {
                    return new ValidationResult(ErrorMessage ?? $"The date cannot be further than {_minDate:dd/MM/yyyy}.");
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
            MergeAttribute(context.Attributes, "data-val-mindate", ErrorMessage ?? $"The date cannot be further than {_minDate:dd/MM/yyyy}.");
            MergeAttribute(context.Attributes, "data-val-mindate-min", _minDate.ToString("yyyy-MM-dd"));
        }
    }
}
