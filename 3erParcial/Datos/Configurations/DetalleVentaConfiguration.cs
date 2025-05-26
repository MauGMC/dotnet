namespace Datos.Configurations
{
    public class DetalleVentaConfiguration : IEntityTypeConfiguration<DetalleVenta>
    {
        public void Configure(EntityTypeBuilder<DetalleVenta> builder)
        {
            //Tabla
            builder.ToTable("detalle_ventas");
            //PK
            builder.HasKey(dv => dv.DetalleVentaID);
            //FKs
            builder.HasOne(dv => dv.Venta)
                .WithMany(v => v.DetallesVentas)
                .HasForeignKey(dv => dv.VentaID)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(dv => dv.Producto)
                .WithMany(p => p.DetallesVentas)
                .HasForeignKey(dv => dv.ProductoID)
                .OnDelete(DeleteBehavior.Restrict);
            //Atributos
            builder.Property(dv => dv.DetalleVentaID)
                .HasColumnName("detalle_venta_id")
                .ValueGeneratedOnAdd();
            builder.Property(dv => dv.VentaID)
                .HasColumnName("venta_id")
                .IsRequired();
            builder.Property(dv => dv.ProductoID)
                .HasColumnName("producto_id")
                .IsRequired();
            builder.Property(dv => dv.Cantidad)
                .HasColumnName("cantidad")
                .IsRequired();
            builder.Property(dv => dv.PrecioUnitario)
                .HasColumnName("precio_unitario")
                .IsRequired();
            builder.Property(dv => dv.Subtotal)
                .HasColumnName("subtotal")
                .IsRequired();
            //Indices
            builder.HasIndex(dv => dv.VentaID);
            builder.HasIndex(dv => dv.ProductoID);
        }
    }
}
