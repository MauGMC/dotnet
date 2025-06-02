using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Presentacion.Validaciones
{
    public class TodayOnwardsAttribute : ValidationAttribute, IClientModelValidator
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is DateTime fecha)
            {
                if(fecha > DateTime.Today)
                {
                    return new ValidationResult(ErrorMessage ?? "The date must be today or before.");
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
            MergeAttribute(context.Attributes, "data-val-todayonwards", ErrorMessage ?? "The date must be today or before.");
            MergeAttribute(context.Attributes, "data-val-todayonwards-today", DateTime.Today.ToString("yyyy-MM-dd"));
        }
    }
}
