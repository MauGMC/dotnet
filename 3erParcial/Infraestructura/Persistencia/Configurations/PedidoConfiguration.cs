namespace Infraestructura.Persistencia.Configurations
{
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            //Tabla
            builder.ToTable("pedidos");
            //PK
            builder.HasKey(p => p.PedidoID);
            //FKs
            builder.HasOne(p => p.Cliente)
                .WithMany(c => c.Pedidos)
                .HasForeignKey(p => p.ClienteID)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.Direccion)
                .WithMany(d => d.Pedidos)
                .HasForeignKey(p => p.DireccionID)
                .OnDelete(DeleteBehavior.Restrict);
            //Propiedades de navegación EF
            builder.HasMany(p => p.DetallesPedidos)
                .WithOne(dp => dp.Pedido);
            //Atributos
            builder.Property(p => p.PedidoID)
                .HasColumnName("pedido_id")
                .ValueGeneratedOnAdd();
            builder.Property(p => p.ClienteID)
                .HasColumnName("cliente_id")
                .IsRequired();
            builder.Property(p => p.DireccionID)
                .HasColumnName("direccion_id")
                .IsRequired();
            builder.Property(p => p.FechaPedido)
                .HasColumnName("fecha_pedido")
                .IsRequired();
            builder.Property(p => p.FechaEntrega)
                .HasColumnName("fecha_entrega")
                .IsRequired(false);
            builder.Property(p => p.Estado)
                .HasColumnName("estado")
                .IsRequired()
                .HasDefaultValue(1);
            builder.Property(p => p.FechaExpiracion)
                .HasColumnName("fecha_expiracion")
                .IsRequired();
            builder.Property(p => p.TotalSinIVA)
                .HasColumnName("total_sin_iva")
                .IsRequired()
                .HasPrecision(18, 2);
            builder.Property(p => p.IVA)
                .HasColumnName("iva")
                .IsRequired()
                .HasPrecision(18, 2);
            builder.Property(p => p.TotalConIVA)
                .HasColumnName("total_con_iva")
                .IsRequired()
                .HasPrecision(18, 2);
            builder.Property(p => p.Comentarios)
                .HasColumnName("comentarios")
                .IsRequired(false);
            //Indices
            builder.HasIndex(p => p.ClienteID);
            builder.HasIndex(p => p.DireccionID);
        }
    }
}
