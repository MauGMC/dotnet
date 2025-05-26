namespace Datos.Configurations
{
    public class CarritoConfiguration : IEntityTypeConfiguration<Carrito>
    {
        public void Configure(EntityTypeBuilder<Carrito> builder)
        {
            //Tabla
            builder.ToTable("carrito");
            //PK
            builder.HasKey(c => c.CarritoID);
            //FKs
            builder.HasOne(ca=>ca.Cliente)
                .WithOne(cl => cl.Carrito)
                .HasForeignKey<Carrito>(ca => ca.ClienteID)
                .OnDelete(DeleteBehavior.Restrict);
            //Atributos
            builder.Property(c => c.CarritoID)
                .HasColumnName("carrito_id")
                .ValueGeneratedOnAdd();
            builder.Property(c => c.ClienteID)
                .HasColumnName("cliente_id")
                .IsRequired();
            builder.Property(c => c.FechaCreacion)
                .HasColumnName("fecha_creacion")
                .IsRequired();
            builder.Property(c => c.FechaExpiracion)
                .HasColumnName("fecha_expiracion")
                .IsRequired(false);
            builder.Property(c => c.Estado)
                .HasColumnName("estado")
                .IsRequired();
            //Indices
            builder.HasIndex(c => c.ClienteID);

        }
    }
}
