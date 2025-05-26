namespace Datos.Configurations
{
    public class NotificacionConfiguration : IEntityTypeConfiguration<Notificacion>
    {
        public void Configure(EntityTypeBuilder<Notificacion> builder)
        {
            // Tabla
            builder.ToTable("notificaciones");
            // PK
            builder.HasKey(n => n.NotificacionID);
            //Propiedades de navegación EF
            builder.HasMany(n => n.NotificacionesPorUsuarios)
                .WithOne(npu => npu.Notificacion);
            // Atributos
            builder.Property(n => n.NotificacionID)
                .HasColumnName("notificacion_id")
                .ValueGeneratedOnAdd();
            builder.Property(n => n.Titulo)
                .HasColumnName("titulo")
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(n => n.Mensaje)
                .HasColumnName("mensaje")
                .IsRequired()
                .HasMaxLength(500);
            builder.Property(n => n.Fecha)
                .HasColumnName("fecha")
                .IsRequired();
            builder.Property(n => n.Tipo)
                .HasColumnName("tipo")
                .IsRequired(false)
                .HasMaxLength(50);
            
        }
    }
}
