namespace Infraestructura.Persistencia.Configurations
{
    public class DetallePedidoConfiguration : IEntityTypeConfiguration<DetallePedido>
    {
        public void Configure(EntityTypeBuilder<DetallePedido> builder)
        {
            //Tabla
            builder.ToTable("detalle_pedidos");
            //PK
            builder.HasKey(dp => dp.DetallePedidoID);
            //FKs
            builder.HasOne(dp => dp.Pedido)
                .WithMany(p => p.DetallesPedidos)
                .HasForeignKey(dp => dp.PedidoID)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(dp => dp.Producto)
                .WithMany(p => p.DetallesPedidos)
                .HasForeignKey(dp => dp.ProductoID)
                .OnDelete(DeleteBehavior.Restrict);
            //Atributos
            builder.Property(dp => dp.DetallePedidoID)
                .HasColumnName("detalle_pedido_id")
                .ValueGeneratedOnAdd();
            builder.Property(dp => dp.PedidoID)
                .HasColumnName("pedido_id")
                .IsRequired();
            builder.Property(dp => dp.ProductoID)
                .HasColumnName("producto_id")
                .IsRequired();
            builder.Property(dp => dp.Cantidad)
                .HasColumnName("cantidad")
                .IsRequired();
            builder.Property(dp => dp.PrecioUnitario)
                .HasColumnName("precio_unitario")
                .IsRequired();
            builder.Property(dp => dp.Subtotal)
                .HasColumnName("subtotal")
                .IsRequired();
            //Indices
            builder.HasIndex(dp => dp.PedidoID);
            builder.HasIndex(dp => dp.ProductoID);
        }
    }
}
