namespace Infraestructura.Persistencia.Configurations
{
    public class VentaConfiguration : IEntityTypeConfiguration<Venta>
    {
        public void Configure(EntityTypeBuilder<Venta> builder)
        {
            //Tabla
            builder.ToTable("ventas");
            //PK
            builder.HasKey(v => v.VentaID);
            //FKs
            builder.HasOne(v => v.Cliente)
                .WithMany(c => c.Ventas)
                .HasForeignKey(v => v.ClienteID)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(v => v.MetodoDePago)
                .WithMany(mp => mp.Ventas)
                .HasForeignKey(v => v.MetodoPagoID)
                .OnDelete(DeleteBehavior.Restrict);
            //Propiedades de navegación EF
            builder.HasMany(v => v.DetallesVentas)
                .WithOne(dv => dv.Venta);
            //Atributos
            builder.Property(v => v.VentaID)
                .HasColumnName("venta_id")
                .ValueGeneratedOnAdd();
            builder.Property(v => v.ClienteID)
                .HasColumnName("cliente_id")
                .IsRequired();
            builder.Property(v => v.MetodoPagoID)
                .HasColumnName("metodo_pago_id")
                .IsRequired();
            builder.Property(v => v.FechaVenta)
                .HasColumnName("fecha_venta")
                .IsRequired();
            builder.Property(v => v.TotalSinIVA)
                .HasColumnName("total_sin_iva")
                .IsRequired()
                .HasDefaultValue(0);
            builder.Property(v => v.IVA)
                .HasColumnName("iva")
                .IsRequired()
                .HasDefaultValue(0);
            builder.Property(v => v.TotalConIVA)
                .HasColumnName("total_con_iva")
                .IsRequired()
                .HasDefaultValue(0);
            builder.Property(v => v.Estado)
                .HasColumnName("estado")
                .HasDefaultValue(1)
                .IsRequired();
        }
    }
}
