namespace Infraestructura.Persistencia.Configurations
{
    public class ProveedorConfiguration : IEntityTypeConfiguration<Proveedor>
    {
        public void Configure(EntityTypeBuilder<Proveedor> builder)
        {
            //Tabla
            builder.ToTable("proveedores");
            //PK
            builder.HasKey(p => p.ProveedorID);
            //Propiedades de navegación EF
            builder.HasMany(p => p.Compras)
                .WithOne(c => c.Proveedor);
            //Atributos
            builder.Property(p => p.ProveedorID)
                .HasColumnName("proveedor_id")
                .ValueGeneratedOnAdd();
            builder.Property(p => p.Nombre)
                .HasColumnName("nombre")
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(p => p.Telefono)
                .HasColumnName("telefono")
                .IsRequired()
                .HasMaxLength(15);
            builder.OwnsOne(u => u.Email, email =>
            {
                email.Property(e => e.Valor)
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsRequired();
            });
            builder.Property(p => p.Direccion)
                .HasColumnName("direccion")
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(p => p.Activo)
                .HasColumnName("activo")
                .IsRequired()
                .HasDefaultValue(false);

        }
    }
}
