using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infraestructura.DbConfigurations
{
    public class ProductoConfiguration : IEntityTypeConfiguration<Producto>
    {

        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            //Tabla
            builder
                .ToTable("ProductosEF")
                .HasKey(p => p.ProductoID);
            //Columnas
            builder
                .Property(producto => producto.ProductoID)
                .HasColumnName("ProductoID")
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasColumnType("int unsigned");
            builder
                .Property(producto => producto.Nombre)
                 .HasColumnName("Nombre")
                 .IsRequired()
                 .HasColumnType("varchar(30)");
            builder.HasIndex(producto => producto.Nombre)
                .IsUnique()
                .HasDatabaseName("IX_ProductosEF_Nombre");
            builder
                .Property(producto => producto.Descripcion)
                .HasColumnName("Descripcion")
                .HasColumnType("varchar(100)")
                .IsRequired(false);
            builder
                .Property(producto => producto.Cantidad)
                .HasColumnName("Cantidad")
                .IsRequired()
                .HasColumnType("int unsigned");
            builder
                .Property(producto => producto.Precio)
                .HasColumnName("Precio")
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            builder
                .Property(producto => producto.Categoria)
                .HasColumnName("Categoria")
                .IsRequired()
                .HasColumnType("int unsigned");
            builder
                .Property(producto => producto.FechaCreacion)
                .HasColumnName("FechaCreacion")
                .IsRequired()
                .HasColumnType("datetime");
            builder
                .Property(producto => producto.RutaImagen)
                .HasColumnName("RutaImagen")
                .HasColumnType("varchar(255)")
                .IsRequired(false);
        }

    }
}
