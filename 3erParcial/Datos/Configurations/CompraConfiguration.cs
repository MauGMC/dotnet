namespace Datos.Configurations
{
    public class CompraConfiguration : IEntityTypeConfiguration<Compra>
    {
        public void Configure(EntityTypeBuilder<Compra> builder)
        {
            //Tabla
            builder.ToTable("compras");
            //PK
            builder.HasKey(c => c.CompraID);
            //FKs
            builder.HasOne(c=>c.Proveedor)
                .WithMany(p=>p.Compras)
                .HasForeignKey(c => c.ProveedorID)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(c=>c.Empleado)
                .WithMany(e => e.Compras)
                .HasForeignKey(c => c.EmpleadoID)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(c => c.MetodoDePago)
                .WithMany(mp => mp.Compras)
                .HasForeignKey(c => c.MetodoDePagoID)
                .OnDelete(DeleteBehavior.Restrict);
            //Propiedades de navegación EF
            builder.HasMany(c => c.DetallesCompras)
                .WithOne(dc => dc.Compra);
            //Atributos
            builder.Property(c => c.CompraID)
                .HasColumnName("compra_id")
                .ValueGeneratedOnAdd()
                .IsRequired();
            builder.Property(c => c.ProveedorID)
                .HasColumnName("proveedor_id")
                .IsRequired();
            builder.Property(c => c.EmpleadoID)
                .HasColumnName("empleado_id")
                .IsRequired()
                .HasMaxLength(450);
            builder.Property(c => c.MetodoDePagoID)
                .HasColumnName("metodo_pago_id")
                .IsRequired();
            builder.Property(c => c.FechaCompra)
                .HasColumnName("fecha_compra")
                .IsRequired();
            builder.Property(c => c.FechaEntrega)
                .HasColumnName("fecha_entrega")
                .IsRequired(false);
            builder.Property(c => c.NumeroFactura)
                .HasColumnName("numero_factura")
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.Estado)
                .HasColumnName("estado")
                .HasDefaultValue(1)
                .IsRequired();
            builder.Property(c => c.Total)
                .HasColumnName("total")
                .HasPrecision(18, 2)
                .IsRequired();
            //Indices
            builder.HasIndex(c=>c.NumeroFactura)
                .IsUnique()
                .HasDatabaseName("IX_NumeroFactura");
            builder.HasIndex(c => c.ProveedorID);
            builder.HasIndex(c=>c.EmpleadoID);
        }
    }
}
