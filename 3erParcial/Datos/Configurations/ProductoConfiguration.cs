namespace Datos.Configurations
{
    public class ProductoConfiguration : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            //Tabla
            builder.ToTable("productos");
            //PK
            builder.HasKey(p => p.ProductoID);
            //Propiedades de navegación EF
            builder.HasOne(p => p.Inventario)
                .WithOne(i => i.Producto);
            //Atributos
            builder.Property(p => p.ProductoID)
                .HasColumnName("producto_id")
                .ValueGeneratedOnAdd();
            builder.Property(p => p.Nombre)
                .HasColumnName("nombre")
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(p => p.Categoria)
                .HasColumnName("categoria")
                .IsRequired();
            builder.Property(p => p.Descripcion)
                .HasColumnName("descripcion")
                .IsRequired(false);
            builder.Property(p => p.Precio)
                .HasColumnName("precio")
                .IsRequired();
            builder.Property(p => p.Stock)
                .HasColumnName("stock")
                .IsRequired();
            builder.Property(p => p.FechaCreacion)
                .HasColumnName("fecha_creacion")
                .IsRequired();
            //Indices
            builder.HasIndex(p => p.Nombre);
        }
    }
}
