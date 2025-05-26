namespace Infraestructura.Persistencia.Configurations
{
    public class ImagenConfiguration : IEntityTypeConfiguration<Imagen>
    {
        public void Configure(EntityTypeBuilder<Imagen> builder)
        {
            //Tabla
            builder.ToTable("imagenes");
            //PK
            builder.HasKey(i => i.ImagenID);
            //Atributos
            builder.Property(i => i.ImagenID)
                .HasColumnName("imagen_id")
                .ValueGeneratedOnAdd();
            builder.Property(i => i.NombreArchivo)
                .HasColumnName("producto_id")
                .IsRequired();
            builder.Property(i => i.RutaImagen)
                .HasColumnName("ruta_imagen")
                .IsRequired();
            builder.Property(i=>i.TablaOrigen)
                .HasColumnName("tabla_origen")
                .IsRequired();
            builder.Property(i => i.IDOrigen)
                .HasColumnName("id_origen")
                .IsRequired();
        }
    }
}
