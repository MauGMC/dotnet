namespace Datos.Configurations
{
    public class DetalleCompraConfiguration : IEntityTypeConfiguration<DetalleCompra>
    {
        public void Configure(EntityTypeBuilder<DetalleCompra> builder)
        {
            //Tabla
            builder.ToTable("detalles_compras");
            //PK
            builder.HasKey(dc => dc.DetalleCompraID);
            //FKs
            builder.HasOne(dc => dc.Compra)
                .WithMany(c => c.DetallesCompras)
                .HasForeignKey(dc => dc.CompraID)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(dc => dc.Producto)
                .WithMany(p => p.DetallesCompras)
                .HasForeignKey(dc => dc.ProductoID)
                .OnDelete(DeleteBehavior.Restrict);
            //Atributos
            builder.Property(dc => dc.DetalleCompraID)
                .HasColumnName("detalle_compra_id")
                .ValueGeneratedOnAdd();
            builder.Property(dc => dc.CompraID)
                .HasColumnName("compra_id")
                .IsRequired();
            builder.Property(dc => dc.ProductoID)
                .HasColumnName("producto_id")
                .IsRequired();
            builder.Property(dc => dc.Cantidad)
                .HasColumnName("cantidad")
                .IsRequired();
            builder.Property(dc => dc.PrecioUnitario)
                .HasColumnName("precio_unitario")
                .IsRequired();
            builder.Property(dc => dc.Subtotal)
                .HasColumnName("subtotal")
                .IsRequired();
            //Indices
            builder.HasIndex(dc => dc.CompraID);
            builder.HasIndex(dc => dc.ProductoID);
        }
    }
}
