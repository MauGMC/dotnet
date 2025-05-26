namespace Infraestructura.Persistencia.Configurations
{
    public class ProductoEnCarritoConfiguration : IEntityTypeConfiguration<ProductoEnCarrito>
    {
        public void Configure(EntityTypeBuilder<ProductoEnCarrito> builder)
        {
            //Tabla
            builder.ToTable("productos_en_carrito");
            //PK
            builder.HasKey(p => p.ProductoEnCarritoID);
            //FKs
            builder.HasOne(p => p.Carrito)
                .WithMany(c => c.ProductosEnCarrito)
                .HasForeignKey(p => p.CarritoID)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.Producto)
                .WithMany(pr => pr.ProductosEnCarrito)
                .HasForeignKey(p => p.ProductoID)
                .OnDelete(DeleteBehavior.Restrict);
            //Atributos
            builder.Property(p => p.ProductoEnCarritoID)
                .HasColumnName("producto_en_carrito_id")
                .ValueGeneratedOnAdd();
            builder.Property(p => p.CarritoID)
                .HasColumnName("carrito_id")
                .IsRequired();
            builder.Property(p => p.ProductoID)
                .HasColumnName("producto_id")
                .IsRequired();
            builder.Property(p => p.Cantidad)
                .HasColumnName("cantidad")
                .IsRequired();
            builder.Property(p => p.PrecioUnitario)
                .HasColumnName("precio_unitario")
                .IsRequired();
            builder.Property(p => p.Subtotal)
                .HasColumnName("subtotal")
                .IsRequired();
            //Indices
            builder.HasIndex(p => p.CarritoID);
            builder.HasIndex(p => p.ProductoID);
        }
    }
}
