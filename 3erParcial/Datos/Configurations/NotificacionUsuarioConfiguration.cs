namespace Datos.Configurations
{
    public class NotificacionUsuarioConfiguration : IEntityTypeConfiguration<NotificacionPorUsuario>
    {
        public void Configure(EntityTypeBuilder<NotificacionPorUsuario> builder)
        {
            //Tabla
            builder.ToTable("notificaciones_por_usuarios");
            //PK
            builder.HasKey(nu=>nu.NotificacionPorUsuarioID);
            //FKs
            builder.HasOne(n => n.Notificacion)
                .WithMany(nu => nu.NotificacionesPorUsuarios)
                .HasForeignKey(n => n.NotificacionID)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(u => u.Usuario)
                .WithMany(nu => nu.NotificacionesPorUsuarios)
                .HasForeignKey(u => u.UsuarioID)
                .OnDelete(DeleteBehavior.Restrict);
            //Atributos
            builder.Property(nu => nu.NotificacionPorUsuarioID)
                .HasColumnName("notificacion_por_usuario_id")
                .ValueGeneratedOnAdd();
            builder.Property(nu => nu.NotificacionID)
                .HasColumnName("notificacion_id")
                .IsRequired();
            builder.Property(nu => nu.UsuarioID)
                .HasColumnName("usuario_id")
                .IsRequired();
            builder.Property(nu => nu.Leido)
                .HasColumnName("leido")
                .IsRequired()
                .HasDefaultValue(false);
            builder.Property(nu => nu.FechaCreacion)
                .HasColumnName("fecha_creacion")
                .IsRequired();
            builder.Property(nu => nu.FechaLeida)
                .HasColumnName("fecha_leido")
                .IsRequired(false);
            //Indices
            builder.HasIndex(nu => nu.NotificacionID);
            builder.HasIndex(nu => nu.UsuarioID);
        }
    }
}
