namespace Infraestructura.Persistencia.Configurations
{
    public class InventarioConfiguration : IEntityTypeConfiguration<Inventario>
    {
        public void Configure(EntityTypeBuilder<Inventario> builder)
        {
            //Tabla
            builder.ToTable("inventarios");
            //PK
            builder.HasKey(i => i.InventarioID);
            //FKs
            builder.HasOne(i => i.Producto)
                .WithOne(p => p.Inventario)
                .HasForeignKey<Inventario>(i => i.ProductoID)
                .OnDelete(DeleteBehavior.Restrict);
            //Atributos
            builder.Property(i => i.InventarioID)
                .HasColumnName("inventario_id")
                .ValueGeneratedOnAdd();
            builder.Property(i => i.ProductoID)
                .HasColumnName("producto_id")
                .IsRequired();
            builder.Property(i => i.Cantidad)
                .HasColumnName("cantidad")
                .IsRequired();
            builder.Property(i => i.CantidadMinima)
                .HasColumnName("cantidad_minima")
                .IsRequired();
            builder.Property(i => i.CantidadMaxima)
                .HasColumnName("cantidad_maxima")
                .IsRequired();
            builder.Property(i => i.Ubicacion)
                .HasColumnName("ubicacion")
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(i => i.FechaUltimoMovimiento)
                .HasColumnName("fecha_ultimo_movimiento")
                .IsRequired();
            builder.Property(i => i.Estado)
                .HasColumnName("estado")
                .HasDefaultValue(1)
                .IsRequired();
            builder.Property(i => i.Activo)
                .HasColumnName("activo")
                .IsRequired()
                .HasDefaultValue(true);
            //Indices
            builder.HasIndex(i => i.ProductoID);
        }
    }
}
