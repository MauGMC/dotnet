namespace Datos.Configurations
{
    public class DireccionConfiguration : IEntityTypeConfiguration<Direccion>
    {
        public void Configure(EntityTypeBuilder<Direccion> builder)
        {
            //Tabla
            builder.ToTable("direcciones");
            //PK
            builder.HasKey(d => d.DireccionID);
            //Atributos
            builder.Property(d => d.DireccionID)
                .HasColumnName("direccion_id")
                .ValueGeneratedOnAdd();
            builder.Property(d => d.Calle)
                .HasColumnName("calle")
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(d => d.NumeroExterior)
                .HasColumnName("numero_exterior")
                .IsRequired()
                .HasMaxLength(10);
            builder.Property(d => d.NumeroInterior)
                .HasColumnName("numero_interior")
                .IsRequired()
                .HasMaxLength(10);
            builder.Property(d => d.Colonia)
                .HasColumnName("colonia")
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(d => d.CodigoPostal)
                .HasColumnName("codigo_postal")
                .IsRequired()
                .HasMaxLength(10);
            builder.Property(d => d.Ciudad)
                .HasColumnName("ciudad")
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(d => d.Estado)
                .HasColumnName("estado")
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
