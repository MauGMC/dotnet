using FluentValidation;
using Aplicacion.DTOs;

namespace Aplicacion.Validaciones
{
    public class ProductoDetalleValidator : AbstractValidator<ProductoDetalleDTO>
    {
        public ProductoDetalleValidator() 
        {
            RuleFor(x => x.Nombre)
                .NotEmpty()
                .WithMessage("El nombre es obligatorio.")
                .Length(3, 50)
                .WithMessage("El nombre debe tener entre 3 y 50 caracteres.");                
            RuleFor(x => x.Descripcion)
                .Length(0, 100)
                .WithMessage("La descripción debe ser vacía o hasta 100 caracteres.");
            RuleFor(x => x.Precio)
                .NotEmpty()
                .WithMessage("El precio es obligatorio")
                .GreaterThan(0.01m)
                .WithMessage("El precio debe ser mayor que cero.");
            RuleFor(x => x.Cantidad)
                .NotEmpty()
                .WithMessage("La cantidad es obligatoria")
                .GreaterThan(0)
                .WithMessage("La cantidad debe ser mayor que cero.");
            RuleFor(x => x.FechaCreacion)
                .NotEmpty()
                .WithMessage("La fecha de ingreso es obligatoria.")
                .GreaterThan(DateTime.Now)
                .WithMessage("La fecha de ingreso no puede ser futura.");
            RuleFor(x => x.Categoria)
                .NotEmpty()
                .WithMessage("La categoría es obligatoria.")
                .GreaterThan(0)
                .WithMessage("La categoría debe ser mayor que cero.");


        }
    }
}
